using FakeWeatherBackend.Models.API.DTOs.Files;

namespace FakeWeatherBackend.Models.Files;

/// <summary>
/// File info
/// </summary>
public class FileInfo
{
    /// <summary>
    /// File ID
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Filename
    /// </summary>
    public string Name { get; private set; }

    public FileInfo
    (
        Guid id,
        string name
    )
    {
        Id = id;

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Incorrect filename!", nameof(name));
        }

        Name = name;
    }

    /// <summary>
    /// Convert model to DTO
    /// </summary>
    public FileInfoDto ToDto()
    {
        return new FileInfoDto
        (
            Id,
            Name
        );
    }
}