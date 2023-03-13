using Microsoft.AspNetCore.Mvc;
using Shared;
using Newtonsoft.Json;

namespace Learn.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "{numberOfDays}")]
    public async Task<IEnumerable<WeatherForecast>> Get(int numberOfDays)
    {
        using (var client = new HttpClient())
        {
            var url = $"https://learn-weather.azurewebsites.net/api/LearnRandomWeather?numberOfDays={numberOfDays}";
            var key = "6A4eVgL/eS7IEiuEHPhBnqAAqWKJfiUdw3I9mpPT0Qq/Oiw7aBP6PA==";

            using (var requestMessage =
                        new HttpRequestMessage(HttpMethod.Get, url))
            {
                requestMessage.Headers.Add("x-functions-key", key);                
                var response = await client.SendAsync(requestMessage);
                string result = await response.Content.ReadAsStringAsync();
                var forecast = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(result);
                return forecast;
            }
        }
    }
}
