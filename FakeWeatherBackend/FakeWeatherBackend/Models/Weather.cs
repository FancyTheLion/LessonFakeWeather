using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Models.API.DTOs;
using FakeWeatherBackend.Models.Comments;

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
    
    /// <summary>
    /// Weather photo preview
    /// </summary>
    public File PhotoPreview { get; private set; }

    /// <summary>
    /// Comments to weather
    /// </summary>
    public IList<Comment> Comments { get; private set; }

    /// <summary>
    /// Primitive constructor - ID only
    /// </summary>
    public Weather
    (
        Guid id
    )
    {
        Id = id;
        Photo = new File();
        PhotoPreview = new File();
    }
    
    /// <summary>
    /// Normal constructor - all data
    /// </summary>
    public Weather
    (
        Guid id,
        DateTime timestamp,
        double temperature,
        double cloudiness,
        double humidity,
        double pressure,
        File photo,
        File photoPreview,
        IReadOnlyCollection<Comment> comments)
    {
        Id = id;
        Timestamp = timestamp;
        Temperature = temperature;
        Cloudiness = cloudiness;
        Humidity = humidity;
        Pressure = pressure;
        Photo = photo ?? throw new ArgumentNullException(nameof(photo), "Photo mustn't be null!");
        PhotoPreview = photoPreview ?? throw new ArgumentNullException(nameof(photoPreview), "Photo preview mustn't be null!");
        Comments = (comments ?? throw new ArgumentNullException(nameof(comments), "Comments must not be null!")).ToList();
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
            Photo.Id,
            PhotoPreview.Id
        );
    }
}