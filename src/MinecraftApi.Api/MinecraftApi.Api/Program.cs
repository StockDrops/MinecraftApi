using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using MinecraftApi.Ef.Models;
using MinecraftApi.Api.Extensions;
using MinecraftApi.Ef.Models.Contexts;
using MinecraftApi.Ef.Services;
using MinecraftApi.Api.Services;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Rcon.Services;
using MinecraftApi.Core.Rcon.Contracts.Services;
using MinecraftApi.Core.Rcon.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
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

builder.Services.AddScoped<PluginService>();
builder.Services.AddScoped<ArgumentService>();
builder.Services.AddScoped<ICommandService, CommandService>();
builder.Services.AddScoped<IRconClientService, RconClientService>();
builder.Services.AddScoped<IRconCommandService, RconCommandService>();
builder.Services.AddScoped<ICommandExecutionService, CommandExecutionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//migrate the database:

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Migrate the database //TODO: make this optional in case the user prefers to manage it themselves.
app.MigrateDatabase<PluginContext>();
app.SeedData<PluginContext>();
app.Run();
