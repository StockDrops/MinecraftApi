using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using MinecraftApi.Core.Ef.Models;
using MinecraftApi.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
// load the database options
var opts = builder.Configuration.GetSection(nameof(DatabaseConfigurationOptions)).Get<DatabaseConfigurationOptions>();
// replace the password with the real password stored in secrets
opts.ConnectionString = opts.ConnectionString.Replace("[DB_PW]", builder.Configuration["DB_PW"]);
builder.Services.Configure<DatabaseConfigurationOptions>(builder.Configuration.GetSection(nameof(DatabaseConfigurationOptions)));
//Add the database:
builder.Services.AddDbContext<PluginContext>(options =>
{
    switch (opts.DatabaseType)
    {
        case DatabaseType.SqlServer:
            options.UseSqlServer(opts.ConnectionString);
            break;
        case DatabaseType.MySQL:
            var serverVersion = new MySqlServerVersion(opts.MySQLDatabaseVersion);
            options.UseMySql(opts.ConnectionString, serverVersion); // TODO: make this compatible with MySql.
            break;
    }
});
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

app.Run();
