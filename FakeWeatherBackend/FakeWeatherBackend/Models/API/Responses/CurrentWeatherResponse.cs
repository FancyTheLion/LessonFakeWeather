using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs;

namespace FakeWeatherBackend.Models.API.Responses;

/// <summary>
/// Response with the current weather
/// </summary>
public class CurrentWeatherResponse
{
    /// <summary>
    /// Current weather
    /// </summary>
    [JsonPropertyName("currentWeather")]
    public WeatherDto CurrentWeather { get; private set; }

    public CurrentWeatherResponse
    (
        WeatherDto currentWeather
    )
    {
        CurrentWeather = currentWeather ?? throw new ArgumentNullException(nameof(currentWeather), "Current weather mustn't be null!");
    }
}