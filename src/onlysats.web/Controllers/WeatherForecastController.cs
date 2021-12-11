using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Services;

namespace onlysats.web.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly BtcPayServerProxy _BtcPayProxy;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, BtcPayServerProxy btcPayProxy)
    {
        _logger = logger;
        _BtcPayProxy = btcPayProxy;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
    [HttpGet("stores")]
    public async Task<IActionResult> GetStores()
    {
        var stores = await _BtcPayProxy.Client.GetStores();
        return Ok(stores);
    }
}
