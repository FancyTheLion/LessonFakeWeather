using System.ComponentModel.DataAnnotations;

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
}