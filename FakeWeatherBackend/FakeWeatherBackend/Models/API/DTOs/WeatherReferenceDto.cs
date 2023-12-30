using System.Text.Json.Serialization;

namespace FakeWeatherBackend.Models.API.DTOs;

/// <summary>
/// Reference to a weather at given time
/// </summary>
public class WeatherReferenceDto
{
    /// <summary>
    /// Reference timestamp
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; private set; }

    /// <summary>
    /// Reference to a weather
    /// </summary>
    [JsonPropertyName("weatherId")]
    public Guid WeatherId { get; private set; }

    public WeatherReferenceDto
    (
        DateTime timestamp,
        Guid weatherId
    )
    {
        Timestamp = timestamp;
        WeatherId = weatherId;
    }
}