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
    
    public Weather
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

    public WeatherDto ToDto()
    {
        return new WeatherDto
            (
                Id,
                Timestamp, 
                Temperature
            );
    }
}