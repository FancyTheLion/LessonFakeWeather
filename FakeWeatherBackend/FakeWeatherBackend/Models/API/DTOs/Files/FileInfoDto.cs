using System.Text.Json.Serialization;

namespace FakeWeatherBackend.Models.API.DTOs.Files;

/// <summary>
/// Information about file
/// </summary>
public class FileInfoDto
{
    /// <summary>
    /// File ID
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; private set; }

    /// <summary>
    /// Filename
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; private set; }
    
    public FileInfoDto
    (
        Guid id,
        string name
    )
    {
        Id = id;
        
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("File name must be populated!", nameof(name));
        }
        Name = name;
    }
}