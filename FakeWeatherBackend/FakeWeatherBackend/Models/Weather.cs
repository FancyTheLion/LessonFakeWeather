using FakeWeatherBackend.Models.API.DTOs;

// We assume that File is FakeWeatherBackend.Models.Files.File 
using File = FakeWeatherBackend.Models.Files.File;

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

    /// <summary>
    /// Water vapor content in the air
    /// </summary>
    public double Humidity { get; private set; }

    /// <summary>
    /// Air pressure
    /// </summary>
    public double Pressure { get; private set;  }

    /// <summary>
    /// Weather photo
    /// </summary>
    public File Photo { get; private set; }

    public Weather
    (
        Guid id,
        DateTime timestamp,
        double temperature,
        double cloudiness,
        double humidity,
        double pressure,
        File photo
    )
    {
        Id = id;
        Timestamp = timestamp;
        Temperature = temperature;
        Cloudiness = cloudiness;
        Humidity = humidity;
        Pressure = pressure;
        Photo = photo ?? throw new ArgumentNullException(nameof(photo), "Photo mustn't be null!");
    }

    public WeatherDto ToDto()
    {
        return new WeatherDto
        (
            Id,
            Timestamp, 
            Temperature,
            Cloudiness,
            Humidity,
            Pressure,
            Photo.Id
        );
    }
}