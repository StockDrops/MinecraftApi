using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using MinecraftApi.Core.Ef.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
var opts = builder.Configuration.Get<DatabaseConfigurationOptions>();
builder.Services.Configure<DatabaseConfigurationOptions>(builder.Configuration.GetSection(nameof(DatabaseConfigurationOptions)));
//Add the database:
builder.Services.AddDbContext<BaseDbContext>(options =>
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

app.Run();
