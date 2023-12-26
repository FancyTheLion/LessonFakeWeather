using FakeWeatherBackend.Models.API.DTOs;

namespace FakeWeatherBackend.Models;

public class Weather
{
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
        DateTime timestamp,
        double temperature
    )
    {
        Timestamp = timestamp;
        Temperature = temperature;
    }

    public WeatherDto ToDto()
    {
        return new WeatherDto
            (
                Timestamp, 
                Temperature
            );
    }
}