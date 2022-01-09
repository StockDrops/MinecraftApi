using MinecraftApi.Core.Rcon.Contracts.Models;

namespace MinecraftApi.Core.Rcon.Contracts.Services;

/// <summary>
/// Definition of the RconClientService
/// </summary>
public interface IRconClientService : IDisposable
{
    /// <summary>
    /// Is the rcon client authenticated with the server.
    /// </summary>
    bool IsAuthenticated { get; }
    /// <summary>
    /// Is the client initialized.
    /// </summary>
    bool IsInitialized { get; }
    /// <summary>
    /// Allows for a password change will the app is running. The client must be re-authenticated in this case, see <see cref="AuthenticateAsync(CancellationToken)"/>
    /// </summary>
    string Password { set; }
    /// <summary>
    /// Authenticates the client.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> AuthenticateAsync(CancellationToken token);
    /// <summary>
    /// Initializes the client.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task InitializeAsync(CancellationToken token);
    /// <summary>
    /// Sends a message asynchronously.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IRconMessage> SendMessageAsync(IRconMessage message, CancellationToken cancellationToken);
}
