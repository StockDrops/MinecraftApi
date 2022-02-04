using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Extensions;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Models.Commands;
using MinecraftApi.Core.Models.Minecraft.Players;
using MinecraftApi.Core.Rcon.Contracts.Services;
using MinecraftApi.Core.Rcon.Models;
using MinecraftApi.Core.Services;
using MinecraftApi.Core.Services.Patreon;
using MinecraftApi.Ef.Models;
using MinecraftApi.Ef.Models.Contexts;
using MinecraftApi.Ef.Services;
using MinecraftApi.Integrations.Contracts.Patreon;
using MinecraftApi.Integrations.Models.Legacy;
using MinecraftApi.Integrations.Services;
using MinecraftApi.Rcon.Services;
using MinecraftBlazingHub.Data;
using MinecraftBlazingHub.Services.Integrations;
using Radzen;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<PatreonServiceOptions>(builder.Configuration.GetSection(nameof(PatreonServiceOptions)));
builder.Services.AddTransient<IBlazorPatreonService, BlazorPatreonService>();
builder.Services.AddTransient<IPatreonService>(services => services.GetRequiredService<IBlazorPatreonService>());

builder.Services.AddTransient<PlayerLinkingService>();

var certificate = new X509Certificate2(builder.Configuration["CertificatePath"], builder.Configuration["CertificatePassword"]);

builder.Services.AddHttpClient<LegacyApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("LegacyApi").GetValue<string>("BaseUrl"));
    client.DefaultRequestHeaders.UserAgent.TryParseAdd(builder.Configuration["UserAgent"]);
})
    .AddClientCertificate(certificate);

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddDownstreamWebApi("LegacyApi", builder.Configuration.GetSection("LegacyApi"))
    
            .AddInMemoryTokenCaches();

builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();

// load the database options
var opts = builder.Configuration.GetSection(nameof(DatabaseConfigurationOptions)).Get<DatabaseConfigurationOptions>();

// replace the password with the real password stored in secrets
opts.ConnectionString = opts.ConnectionString.Replace("[DB_PW]", builder.Configuration["DB_PW"]);

builder.Services.Configure<DatabaseConfigurationOptions>((options) =>
{
    options.ConnectionString = opts.ConnectionString;
    options.Password = builder.Configuration["DB_PW"];
    options.DatabaseType = opts.DatabaseType;
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

builder.Services.AddScoped<NotificationService>();

builder.Services.AddTransient<IRepositoryService<MinecraftPlayer, string>, CrudService<PluginContext, MinecraftPlayer, string>>();
builder.Services.AddTransient<IRepositoryService<LinkedPlayer>, CrudService<PluginContext, LinkedPlayer>>();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();
builder.Services.AddSingleton<WeatherForecastService>();

//RCON
builder.Services.Configure<RconClientServiceOptions>((options) =>
{
    options.Host = builder.Configuration["RconHost"];
    options.Port = int.Parse(builder.Configuration["RconPort"]);
    options.Password = builder.Configuration["RconPassword"];
});

builder.Services.AddTransient<IRepositoryService<BaseRanCommand>, CrudService<PluginContext, BaseRanCommand>>();
builder.Services.AddTransient<IRepositoryService<RanCommand>, CrudService<PluginContext, RanCommand>>();
builder.Services.AddTransient<ICommandService, CommandService>();
builder.Services.AddTransient<IRconCommandService, RconCommandService>();
builder.Services.AddTransient<IRconClientService, RconClientService>();
builder.Services.AddTransient<ICommandExecutionService, CommandExecutionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
