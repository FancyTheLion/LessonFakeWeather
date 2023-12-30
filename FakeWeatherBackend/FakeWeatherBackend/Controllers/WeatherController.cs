using FakeWeatherBackend.Models;
using FakeWeatherBackend.Models.API.DTOs;
using FakeWeatherBackend.Models.API.Responses;
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
                (await _fakeWeatherService.GetLastWeatherReferencesAsync())
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
                (await _fakeWeatherService.GetWeatherById(id)).ToDto()
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
                (await _fakeWeatherService.GetLastWeatherReferenceAsync()).ToDto()
            )
        );
    }

}