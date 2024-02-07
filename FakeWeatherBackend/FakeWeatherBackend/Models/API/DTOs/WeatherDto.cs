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
    public Guid Id { get; set; }

    /// <summary>
    /// Weather time
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Temperature
    /// </summary>
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }
    
    /// <summary>
    /// Clouds cover
    /// </summary>
    [JsonPropertyName("cloudiness")]
    public double Cloudiness { get; set; }
    
    /// <summary>
    /// Water vapor content in the air
    /// </summary>
    [JsonPropertyName("humidity")]
    public double Humidity { get; set; }

    /// <summary>
    /// Air pressure
    /// </summary>
    [JsonPropertyName("pressure")]
    public double Pressure { get; set;  }

    public WeatherDto
    (
        Guid id,
        DateTime timestamp,
        double temperature,
        double cloudiness,
        double humidity,
        double pressure
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
        Humidity = humidity;
        Pressure = pressure;
    }

    public Weather ToModel()
    {
        return new Weather
        (
            Id,
            Timestamp,
            Temperature,
            Cloudiness,
            Humidity,
            Pressure
        );
    }
}