using System.ComponentModel.DataAnnotations;
using FakeWeatherBackend.DAO.Models.Comments;

namespace FakeWeatherBackend.DAO.Models;

/// <summary>
/// One weather measurement
/// </summary>
public class WeatherDbo
{
    /// <summary>
    /// Weather ID
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Weather time
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Temperature
    /// </summary>
    public double Temperature { get; set; }
    
    /// <summary>
    /// Clouds cover
    /// </summary>
    public double Cloudiness { get; set; }
    
    /// <summary>
    /// Water vapor content in the air
    /// </summary>
    public double Humidity { get; set; }

    /// <summary>
    /// Air pressure
    /// </summary>
    public double Pressure { get; set;  }

    /// <summary>
    /// Weather photo
    /// </summary>
    public FileDbo Photo { get; set; }

    /// <summary>
    /// Weather photo preview
    /// </summary>
    public FileDbo PhotoPreview { get; set; }

    /// <summary>
    /// Weather comment
    /// </summary>
    public IList<CommentDbo> Comments { get; set; }
}