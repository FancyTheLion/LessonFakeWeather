namespace FakeWeatherBackend.Models.Files;

/// <summary>
/// Uploaded file model
/// </summary>
public class File
{
    /// <summary>
    /// File ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// File content
    /// </summary>
    public byte[] Content { get; set; }

    /// <summary>
    /// Furry-readable name (with extension)
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// File type
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Last modified date
    /// </summary>
    public DateTime LastModifiedTime { get; set; }

    /// <summary>
    /// SHA-512 hash of file, to use as ETag
    /// </summary>
    public string Hash { get; set; }
}