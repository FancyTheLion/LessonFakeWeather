using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs;

namespace FakeWeatherBackend.Models.API.Responses;

/// <summary>
/// List with weather references
/// </summary>
public class WeatherReferencesListResponse
{
    /// <summary>
    /// Weather references
    /// </summary>
    [JsonPropertyName("weatherReferences")]
    public IReadOnlyCollection<WeatherReferenceDto> WeatherReferences { get; private set; }

    public WeatherReferencesListResponse
    (
        IReadOnlyCollection<WeatherReferenceDto> weatherReferences
    )
    {
        WeatherReferences = weatherReferences ?? throw new ArgumentNullException(nameof(weatherReferences), "Weather references can't be null!");
    }
}