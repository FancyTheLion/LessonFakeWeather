using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs;

namespace FakeWeatherBackend.Models.API.Responses;

/// <summary>
/// Weather response
/// </summary>
public class WeatherResponse
{
    /// <summary>
    /// Weather
    /// </summary>
    [JsonPropertyName("weather")]
    public WeatherDto Weather { get; private set; }

    public WeatherResponse
    (
        WeatherDto weather
    )
    {
        Weather = weather ?? throw new ArgumentNullException(nameof(weather), "Weather mustn't be null!");
    }
}