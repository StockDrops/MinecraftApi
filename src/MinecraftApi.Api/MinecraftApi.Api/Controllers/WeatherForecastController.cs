using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace MinecraftApi.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class WeatherForecastController : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IEnumerable<WeatherForecast> Get()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}