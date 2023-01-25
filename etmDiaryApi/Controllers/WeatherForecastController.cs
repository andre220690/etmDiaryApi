using etmDiaryApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace etmDiaryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        ApplicationDbContext db;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet(Name = "GetWeatherForecastttt")]
        public void Get()
        {
            Console.WriteLine("Get");
        }


    }
}