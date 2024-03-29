using System.ComponentModel.DataAnnotations;

namespace FakeWeatherBackend.DAO.Models;

/// <summary>
/// File, stored in database
/// </summary>
public class FileDbo
{
    /// <summary>
    /// File ID
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Original file name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// MIME type
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    /// File content
    /// </summary>
    public byte[] Content { get; set; }
    
    /// <summary>
    /// SHA-512 of file content, for use as ETag
    /// </summary>
    public string Hash { get; set; }

    /// <summary>
    /// Last modification time
    /// </summary>
    public DateTime LastModifiedTime { get; set; }

    /// <summary>
    /// This file acts as a photo for the next weathers
    /// </summary>
    public IList<WeatherDbo> PhotosForWeathers { get; set; }

    /// <summary>
    /// This file acts as a photo preview for the next weathers
    /// </summary>
    public IList<WeatherDbo> PhotosPreviewsForWeathers { get; set; }
}