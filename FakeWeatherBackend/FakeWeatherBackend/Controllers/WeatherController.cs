using FakeWeatherBackend.Models;
using FakeWeatherBackend.Models.API.DTOs;
using FakeWeatherBackend.Models.API.Responses;
using FakeWeatherBackend.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FakeWeatherBackend.Controllers;

/// <summary>
/// Controller to work with weather
/// </summary>
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IFakeWeatherService _fakeWeatherService;

    public WeatherController(IFakeWeatherService fakeWeatherService)
    {
        _fakeWeatherService = fakeWeatherService;
    }

    /// <summary>
    /// Method to get current weather
    /// </summary>
    [HttpGet]
    [Route("api/Weather/Current")]
    public async Task<ActionResult<CurrentWeatherResponse>> GetCurrentWeather()
    {
        return Ok(new CurrentWeatherResponse((await _fakeWeatherService.GetCurrentWeatherAsync()).ToDto()));
    }
}