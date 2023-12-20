using FakeWeatherBackend.Models.API.DTOs;
using FakeWeatherBackend.Models.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FakeWeatherBackend.Controllers;

/// <summary>
/// Controller to work with weather
/// </summary>
[ApiController]
public class WeatherController : ControllerBase
{
    /// <summary>
    /// Method to get current weather
    /// </summary>
    [HttpGet]
    [Route("api/Weather/Current")]
    public async Task<ActionResult<CurrentWeatherResponse>> GetCurrentWeather()
    {
        return Ok(new CurrentWeatherResponse(new WeatherDto(DateTime.UtcNow, 25)));
    }
}