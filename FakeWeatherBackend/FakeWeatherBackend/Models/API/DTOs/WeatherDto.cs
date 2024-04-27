using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs.Comments;
using FakeWeatherBackend.Models.Comments;
using File = FakeWeatherBackend.Models.Files.File;

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
    
    /// <summary>
    /// Photo ID
    /// </summary>
    [JsonPropertyName("photoId")]
    public Guid PhotoId { get; set; }
    
    /// <summary>
    /// Photo preview ID
    /// </summary>
    [JsonPropertyName("photoPreviewId")]
    public Guid PhotoPreviewId { get; set; }

    public WeatherDto
    (
        Guid id,
        DateTime timestamp,
        double temperature,
        double cloudiness,
        double humidity,
        double pressure,
        Guid photoId,
        Guid photoPreviewId
    )
    {
        
        Id = id;
        Timestamp = timestamp;
        Temperature = temperature;
        Cloudiness = cloudiness;
        Humidity = humidity;
        Pressure = pressure;
        PhotoId = photoId;
        PhotoPreviewId = photoPreviewId;
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
            Pressure,
            new File() { Id = PhotoId },
            new File() { Id = Guid.Empty }, // Nonexistent preview
            new List<Comment>() // We can't get comments from WeatherDTO
        );
    }
}