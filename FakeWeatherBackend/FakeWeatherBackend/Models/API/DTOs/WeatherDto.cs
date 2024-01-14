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
    
    /// <summary>
    /// Clouds cover
    /// </summary>
    [JsonPropertyName("cloudiness")]
    public double Cloudiness { get; private set; }

    public WeatherDto
    (
        Guid id,
        DateTime timestamp,
        double temperature,
        double cloudiness
    )
    {
        Id = id;
        Timestamp = timestamp;
        Temperature = temperature;

        if (cloudiness < 0.0 || cloudiness > 100.0)
        {
            throw new ArgumentOutOfRangeException(nameof(cloudiness), cloudiness, "Cloudiness have to be in [0; 100] range!");
        }

        Cloudiness = cloudiness;
    }
}