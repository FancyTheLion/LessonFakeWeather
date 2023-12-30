using System.Text.Json.Serialization;

namespace FakeWeatherBackend.Models.API.DTOs;

/// <summary>
/// Weather for given time
/// </summary>
public class WeatherDto
{
    /// <summary>
    /// Weather ID
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; private set; }

    /// <summary>
    /// Weather time
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; private set; }

    /// <summary>
    /// Temperature
    /// </summary>
    [JsonPropertyName("temperature")]
    public double Temperature { get; private set; }

    public WeatherDto
    (
        Guid id,
        DateTime timestamp,
        double temperature
    )
    {
        Id = id;
        Timestamp = timestamp;
        Temperature = temperature;
    }
}