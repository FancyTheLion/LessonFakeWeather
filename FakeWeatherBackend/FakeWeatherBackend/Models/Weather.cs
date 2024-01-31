using FakeWeatherBackend.Models.API.DTOs;

namespace FakeWeatherBackend.Models;

public class Weather
{
    /// <summary>
    /// Weather ID
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Weather time
    /// </summary>
    public DateTime Timestamp { get; private set; }

    /// <summary>
    /// Temperature
    /// </summary>
    public double Temperature { get; private set; }
    
    /// <summary>
    /// Clouds cover
    /// </summary>
    public double Cloudiness { get; private set; }
    
    public Weather
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

    public WeatherDto ToDto()
    {
        return new WeatherDto
        (
            Id,
            Timestamp, 
            Temperature,
            Cloudiness
        );
    }
}