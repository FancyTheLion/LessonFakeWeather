using FakeWeatherBackend.Models;
using FakeWeatherBackend.Models.API.DTOs;
using FakeWeatherBackend.Models.API.Requests;
using FakeWeatherBackend.Models.API.Responses;
using FakeWeatherBackend.Models.API.Responses.Settings;
using FakeWeatherBackend.Services.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FakeWeatherBackend.Controllers;

/// <summary>
/// Controller to work with weather
/// </summary>
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }
    
    /// <summary>
    /// Get weather references list
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("api/WeatherReferences")]
    public async Task<ActionResult<WeatherReferencesListResponse>> GetWeatherReferences()
    {
        return Ok
        (
            new WeatherReferencesListResponse
            (
                (await _weatherService.GetLastWeatherReferencesAsync())
                .Select(wr => wr.ToDto()) 
                .ToList()
            )
        );
    }

    /// <summary>
    /// Get any weather by ID
    /// </summary>
    [HttpGet]
    [Route("api/Weather/{id}")]
    public async Task<ActionResult<WeatherResponse>> GetWeatherByIdAsync(Guid id)
    {
        return Ok
        (
            new WeatherResponse
            (
                (await _weatherService.GetWeatherByIdAsync(id)).ToDto()
            )
        );
    }

    /// <summary>
    /// Method that returns a reference to the latest weather
    /// </summary>
    [HttpGet]
    [Route("api/WeatherReference/Last")]
    public async Task<ActionResult<SingleWeatherReferenceResponse>> GetLastWeatherReference()
    {
        return Ok
        (
            new SingleWeatherReferenceResponse
            (
                (await _weatherService.GetLastWeatherReferenceAsync()).ToDto()
            )
        );
    }

    /// <summary>
    /// Add new weather to backend
    /// </summary>
    [HttpPost]
    [Route("api/Weather/Add")]
    public async Task<ActionResult<WeatherAddedResponse>> AddWeatherAsync([FromBody]AddWeatherRequest weatherToAdd)
    {
        if (!_weatherService.ValidateWeather(weatherToAdd.WeatherToAdd))
        {
            return BadRequest("Weather is not valid!");
        }
        
        return Ok
        (
            new WeatherAddedResponse((await _weatherService.AddWeatherAsync(weatherToAdd.WeatherToAdd.ToModel())).ToDto())
        );
    }

    /// <summary>
    /// Get weather validation settings
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("api/Weather/ValidationSettings")]
    public async Task<ActionResult<WeatherValidationSettingsResponse>> GetWeatherValidationSettingsAsync()
    {
        return Ok
        (
            new WeatherValidationSettingsResponse((await _weatherService.GetWeatherValidationSettingsAsync()).ToDto())
        );
    }
}