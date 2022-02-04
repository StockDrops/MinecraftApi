using Bucket4Csharp.Core.Interfaces;
using Bucket4Csharp.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models.Minecraft.Players;
using MinecraftApi.Core.Services.Patreon;
using Microsoft.EntityFrameworkCore;
using MinecraftApi.Ef.Models;
using System.Runtime.Serialization;
using MinecraftApi.Integrations.Models.Patreon;
using MinecraftApi.Integrations.Extensions.Patreon;
using MinecraftApi.Integrations.Contracts.Patreon;
using MinecraftApi.Core.Models;
using System.Web;

namespace MinecraftApi.Integrations.Patreon;

/// <summary>
/// The link request wasn't found.
/// </summary>
public class LinkRequestNotFound : Exception { }
/// <summary>
/// Failed when reading json
/// </summary>
public class CodeResponseFailedToSerialize : Exception
{
    public CodeResponseFailedToSerialize()
    {
    }

    public CodeResponseFailedToSerialize(string? message) : base(message)
    {
    }

    public CodeResponseFailedToSerialize(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected CodeResponseFailedToSerialize(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

/// <summary>
/// Handles OAUTH2 token verification for patreon
/// </summary>
public class PatreonService : IPatreonService
{
    private readonly PatreonServiceOptions options;
    private readonly HttpClient client;
    private readonly ISchedulingBucket bucket;
    private readonly ILogger<PatreonService> logger;
    private readonly IRepositoryService<LinkedPlayer> linkedPlayerRepositoryService;
    private readonly IRepositoryService<MinecraftPlayer, string> minecraftPlayerRepositoryService;
    private readonly IDbContextFactory<PluginContext> dbContextFactory;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="client"></param>
    /// <param name="logger"></param>
    public PatreonService(IOptions<PatreonServiceOptions> options,
        HttpClient client,
        IRepositoryService<LinkedPlayer> linkedPlayerRepositoryService,
        IRepositoryService<MinecraftPlayer, string> minecraftPlayerRepositoryService,
        IDbContextFactory<PluginContext> dbContextFactory,

        ILogger<PatreonService> logger
        )
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options));
        if (client == null)
            throw new ArgumentNullException(nameof(client));
        if (options.Value?.ClientSecret == null)
            throw new ArgumentNullException(nameof(options.Value.ClientSecret));
        if (options.Value?.RedirectUrl == null)
            throw new ArgumentNullException(nameof(options.Value.RedirectUrl));
        if (string.IsNullOrEmpty(options.Value?.ClientId))
            throw new ArgumentNullException(nameof(options.Value.ClientId));

        this.options = options.Value;
        this.client = client;
        this.linkedPlayerRepositoryService = linkedPlayerRepositoryService;
        this.minecraftPlayerRepositoryService = minecraftPlayerRepositoryService;
        this.logger = logger;
        this.dbContextFactory = dbContextFactory;
        bucket = (ISchedulingBucket)IBucket.CreateBuilder().AddLimit(Bandwidth.Simple(3, TimeSpan.FromSeconds(1))).Build();
    }
    /// <summary>
    /// Verifies a code with Patreon API
    /// </summary>
    /// <param name="code"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> VerifyCodeAsync(string code, string uniqueRequestId, string azureId, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(code))
            throw new ArgumentNullException(nameof(code));
        if (string.IsNullOrEmpty(azureId))
            throw new ArgumentNullException(nameof(azureId));
        if (string.IsNullOrEmpty(uniqueRequestId))
            throw new ArgumentNullException(nameof(uniqueRequestId));

        using var context = dbContextFactory.CreateDbContext();

        var request = await context.LinkRequests.FirstOrDefaultAsync(r => r.UniqueId == uniqueRequestId, token);

        if (request == null)
            throw new LinkRequestNotFound();
        if (request.Status == LinkRequestStatus.Completed)
            throw new LinkRequestNotFound();

        var codeResponse = await GetAuthTokenAsync(code, token);
        //
        var createdLinkedPlayer = await LinkPlayerAsync(uniqueRequestId, azureId, false, token);

        var dbToken = codeResponse.ToToken(createdLinkedPlayer.Id);
        context.Tokens.Add(dbToken);
        //set the request as completed
        request.Status = LinkRequestStatus.Completed;

        await context.SaveChangesAsync();
        return true;
    }
    /// <summary>
    /// Links a minecraft player to an external id.
    /// </summary>
    /// <param name="uniqueRequestId"></param>
    /// <param name="externalId"></param>
    /// <param name="setRequestAsCompleted"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="LinkRequestNotFound"></exception>
    public async Task<LinkedPlayer> LinkPlayerAsync(string uniqueRequestId, string externalId, bool setRequestAsCompleted = true, CancellationToken token = default)
    {
        using var context = dbContextFactory.CreateDbContext();

        var request = await context.LinkRequests.FirstOrDefaultAsync(r => r.UniqueId == uniqueRequestId, token);

        if (request == null)
            throw new LinkRequestNotFound();
        if (request.Status == LinkRequestStatus.Completed)
            throw new LinkRequestNotFound();
#if RELEASE
        if(request.ExpirationTime < DateTime.UtcNow)
            throw new LinkRequestNotFound();
#endif
        //TODO: what if the linked player already exists?
        var linkedPlayer = new LinkedPlayer
        {
            ExternalId = externalId,
            PlayerId = request.PlayerId
        };
        var createdLinkedPlayer = await linkedPlayerRepositoryService.CreateAsync(linkedPlayer);

        //set the request as completed
        if(setRequestAsCompleted)
            request.Status = LinkRequestStatus.Completed;

        await context.SaveChangesAsync();
        return createdLinkedPlayer;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="CodeResponseFailedToSerialize"></exception>
    private async Task<CodeResponse> GetAuthTokenAsync(string code, CancellationToken token = default)
    {
        await bucket.TryConsumeAsync(1, TimeSpan.FromMilliseconds(2000), token);
        var request = CreateCodeVerificationRequestMessage(code);
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            try
            {
                var r = await response.Content.ReadFromJsonAsync<CodeResponse>(cancellationToken: token);
                if (r == null)
                    throw new CodeResponseFailedToSerialize($"Something went wrong in the seralization and we couldn't serialize it. No exception was thrown by System.Text.Json.\nOriginal content {await response.Content.ReadAsStringAsync()}");
                return r;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                throw new CodeResponseFailedToSerialize("Failed to serialize the response from the API.", ex);
            }
        }
        else
        {
            logger.LogError("HTTP Request Failed {statusCode} - {reason} - {content}", response.StatusCode, response.ReasonPhrase, await response.Content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();
        }
        throw new InvalidOperationException("Code shouldn't have reached this line.");
    }

    private HttpRequestMessage CreateCodeVerificationRequestMessage(string code)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, options.CodeAuthorizationEndpoint);
        var content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>>
                {
                        new KeyValuePair<string, string>("code", code),
                        new KeyValuePair<string, string>("grant_type", "authorization_code"),
                        new KeyValuePair<string, string>("client_id", options.ClientId),
                        new KeyValuePair<string, string>("client_secret", options.ClientSecret),
                        new KeyValuePair<string, string>("redirect_uri", options.RedirectUrl)
                }
            );
        request.Content = content;
        return request;
    }

    public async Task<LinkRequest> CreateLinkRequestAsync(MinecraftPlayer minecraftPlayer, CancellationToken token = default)
    {
        using var context = dbContextFactory.CreateDbContext();
        var existingPlayer = await minecraftPlayerRepositoryService.CreateOrUpdateAsync(minecraftPlayer);

        var linkRequest = new LinkRequest()
        {
            ExpirationTime = DateTime.UtcNow.AddHours(1),
            PlayerId = existingPlayer.Id,
            RequestedTime = DateTime.UtcNow,
            Status = LinkRequestStatus.Pending
        };
        linkRequest.Oauth2RequestUrl = $"{options.RedirectUrl.TrimEnd('/').TrimEnd('\\')}?state={HttpUtility.UrlEncode(linkRequest.UniqueId)}";
        context.LinkRequests.Add(linkRequest);
        await context.SaveChangesAsync();
        return linkRequest;
    }
}
