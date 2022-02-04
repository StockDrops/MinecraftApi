using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using MinecraftApi.Ef.Models;
using MinecraftApi.Api.Extensions;
using MinecraftApi.Ef.Models.Contexts;
using MinecraftApi.Ef.Services;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Rcon.Services;
using MinecraftApi.Core.Rcon.Contracts.Services;
using MinecraftApi.Core.Rcon.Models;
using Microsoft.AspNetCore.Authorization;
using MinecraftApi.Api.Handlers;
using MinecraftApi.Integrations.Contracts.Patreon;
using MinecraftApi.Integrations.Patreon;
using MinecraftApi.Core.Services.Patreon;
using Microsoft.Extensions.DependencyInjection;
using MinecraftApi.Core.Models.Minecraft.Players;
using MinecraftApi.Core.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MinecraftApi.Core.Models.Configuration;
using OpenStockApi.Core.Models.Configuration;
using MinecraftApi.Core.Models.Commands;
using MinecraftApi.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"),
                                            jwtBearerScheme: JwtBearerDefaults.AuthenticationScheme);
builder.Services.AddAuthorization(options => options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());
// load the database options
var opts = builder.Configuration.GetSection(nameof(DatabaseConfigurationOptions)).Get<DatabaseConfigurationOptions>();
var azureConfiguration = builder.Configuration.GetSection("AzureAdSwagger").Get<AzureB2CConfiguration>();
// replace the password with the real password stored in secrets
opts.ConnectionString = opts.ConnectionString.Replace("[DB_PW]", builder.Configuration["DB_PW"]);

builder.Services.Configure<DatabaseConfigurationOptions>((options) => 
{
    options.ConnectionString = opts.ConnectionString;
    options.Password = builder.Configuration["DB_PW"];
    options.DatabaseType = opts.DatabaseType;
});
builder.Services.Configure<RconClientServiceOptions>((options) =>
{
    options.Host = builder.Configuration["RconHost"];
    options.Port = int.Parse(builder.Configuration["RconPort"]);
    options.Password = builder.Configuration["RconPassword"];
});

//Add the database:
switch (opts.DatabaseType)
{

    case DatabaseType.SqlServer:
        builder.Services.AddDbContext<PluginContext, SqlContext>();
        break;
    case DatabaseType.MySQL:
        builder.Services.AddDbContext<PluginContext, MySqlContext>();
        break;
}
builder.Services.AddDbContextFactory<PluginContext, PluginContextFactory>();

#if DEBUG
if (builder.Environment.IsDevelopment())
{
    //allow anonymous requests for debugging:
    //builder.Services.AddSingleton<IAuthorizationHandler, AllowAnonymousHandler>();
}
#endif

builder.Services.AddHttpClient();

builder.Services.AddScoped<PluginService>();
builder.Services.AddScoped<ArgumentService>();
builder.Services.AddScoped<ICommandService, CommandService>();
builder.Services.AddScoped<IRconClientService, RconClientService>();
builder.Services.AddScoped<IRconCommandService, RconCommandService>();
builder.Services.AddScoped<ICommandExecutionService, CommandExecutionService>();



builder.Services.AddScoped<IRepositoryService<MinecraftPlayer, string>, CrudService<PluginContext, MinecraftPlayer, string>>();
builder.Services.AddScoped<IRepositoryService<LinkedPlayer>, CrudService<PluginContext, LinkedPlayer>>();
builder.Services.AddScoped<IRepositoryService<Command>, CrudService<PluginContext, Command>>();
builder.Services.AddScoped<IRepositoryService<Plugin>, CrudService<PluginContext, Plugin>>();

builder.Services.AddScoped<IRepositoryService<BaseRanCommand>, CrudService<PluginContext, BaseRanCommand>>();
builder.Services.AddScoped<IRepositoryService<RanCommand>, CrudService<PluginContext, RanCommand>>();

builder.Services.Configure<PatreonServiceOptions>(builder.Configuration.GetSection(nameof(PatreonServiceOptions)));
builder.Services.AddScoped<IPatreonService, PatreonService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //c.AddSecurityDefinition("oauth2-ad", new OpenApiSecurityScheme
    //{
    //    Type = SecuritySchemeType.OAuth2,
    //    Flows = new OpenApiOAuthFlows()
    //    {
    //        Implicit = new OpenApiOAuthFlow()
    //        {

    //            AuthorizationUrl = new Uri($"https://login.microsoftonline.com/{azureConfiguration.TenantId}/oauth2/v2.0/authorize"),
    //            TokenUrl = new Uri($"https://login.microsoftonline.com/{azureConfiguration.TenantId}/oauth2/v2.0/token"),
    //            Scopes = new Dictionary<string, string>
    //            {
    //                {
    //                    azureConfiguration.ApiScopeRoot + ApiScopes.Read,
    //                    "Allows for reading from the api."
    //                },
    //                {
    //                    azureConfiguration.ApiScopeRoot + ApiScopes.Write,
    //                    "Write data to the server."
    //                },
    //                {
    //                    azureConfiguration.ApiScopeRoot + ApiScopes.Update,
    //                    "Update existing data"
    //                },
    //                {
    //                    azureConfiguration.ApiScopeRoot + ApiScopes.Delete,
    //                    "Deletes data from the server"
    //                }
    //            }

    //        }
    //    }
    //});
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            Implicit = new OpenApiOAuthFlow()
            {

                AuthorizationUrl = new Uri($"{azureConfiguration.Instance}{azureConfiguration.Domain}/{azureConfiguration.SignUpSignInPolicyId}/oauth2/v2.0/authorize"),
                TokenUrl = new Uri($"{azureConfiguration.Instance}{azureConfiguration.Domain}/{azureConfiguration.SignUpSignInPolicyId}/oauth2/v2.0/token"),
                Scopes = new Dictionary<string, string>
                    {
                        {
                            azureConfiguration.ApiScopeRoot + ApiScopes.Read,
                            "Allows for reading from the api."
                        },
                        {
                            azureConfiguration.ApiScopeRoot + ApiScopes.Write,
                            "Write data to the server."
                        },
                        {
                            azureConfiguration.ApiScopeRoot + ApiScopes.Update,
                            "Update existing data"
                        },
                        {
                            azureConfiguration.ApiScopeRoot + ApiScopes.Delete,
                            "Deletes data from the server"
                        },
                        {
                            azureConfiguration.ApiScopeRoot + ApiScopes.Run,
                            "Run commands on minecraft"
                        }
                    }

            }
        }
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                        },
                        Scheme = "oauth2",
                        Name = "oauth2",
                        In = ParameterLocation.Header
                },
                new List <string>()
            }
        });
    //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    In = ParameterLocation.Header,
    //    Description = "Please enter token",
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.Http,
    //    BearerFormat = "JWT",
    //    Scheme = "bearer"
    //});
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type=ReferenceType.SecurityScheme,
    //                Id="Bearer"
    //            }
    //        },
    //        new string[]{}
    //    }
    //}


    // );

    // Set the comments path for the Swagger JSON and UI.

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//migrate the database:

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {

        //var manifestFile = Assembly.GetExecutingAssembly().GetManifestResourceNames().First(mn => mn.ToLowerInvariant().Contains("swagger"));
        //c.IndexStream = () => Assembly.GetExecutingAssembly().GetManifestResourceStream(manifestFile);
        c.OAuthClientId(azureConfiguration.ClientId);
        c.OAuthUseBasicAuthenticationWithAccessCodeGrant();

    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Migrate the database //TODO: make this optional in case the user prefers to manage it themselves.
app.MigrateDatabase<PluginContext>();
if(app.Environment.IsDevelopment())
    app.SeedData<PluginContext>();
app.Run();
