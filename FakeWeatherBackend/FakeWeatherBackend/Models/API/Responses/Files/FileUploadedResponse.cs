using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs.Files;

namespace FakeWeatherBackend.Models.API.Responses.Files;

/// <summary>
/// Response with information on uploaded file
/// </summary>
public class FileUploadedResponse
{
    [JsonPropertyName("fileInfo")]
    public FileInfoDto FileInfo { get; private set; }
    
    public FileUploadedResponse
    (
        FileInfoDto fileInfo
    )
    {
        FileInfo = fileInfo ?? throw new ArgumentNullException(nameof(fileInfo), "File info must not be null!");
    }
}