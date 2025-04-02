using BusinessInterfaceLayer;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace WepAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoneyForecastController : ControllerBase
    {
        private readonly ITokenRepository _tokenRepository;


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<MoneyForecastController> _logger;

        public MoneyForecastController(ILogger<MoneyForecastController> logger, ITokenRepository tokenRepository, IEnumerable<ITokenRepository> repos)
        {
            _logger = logger;
            _tokenRepository = tokenRepository;

            tokenRepository.LastAccessed = DateTime.UtcNow;
            tokenRepository.LastAccessedBy = "MoneyForecastController";
        }

        [HttpGet(Name = "GetMoneyForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

    }
}
