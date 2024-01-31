using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs;

namespace FakeWeatherBackend.Models.API.Responses;

public class WeatherAddedResponse
{
    /// <summary>
    /// Added weather
    /// </summary>
    [JsonPropertyName("addedWeather")]
    public WeatherDto AddedWeather { get; private set; }

    public WeatherAddedResponse
    (
        WeatherDto addedWeather
    )
    {
        AddedWeather = addedWeather ??
                       throw new ArgumentNullException(nameof(addedWeather), "Weather mustn't be null!");
    }
}