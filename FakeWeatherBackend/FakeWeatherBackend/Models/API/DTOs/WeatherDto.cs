using System.Text.Json.Serialization;

namespace FakeWeatherBackend.Models.API.DTOs;

/// <summary>
/// Weather for given time
/// </summary>
public class WeatherDto
{
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
        DateTime timestamp,
        double temperature
    )
    {
        Timestamp = timestamp;
        Temperature = temperature;
    }
}