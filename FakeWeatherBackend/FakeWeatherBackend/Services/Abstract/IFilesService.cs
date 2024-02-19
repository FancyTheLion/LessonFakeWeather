namespace FakeWeatherBackend.Services.Abstract;

/// <summary>
/// Service for work with files
/// </summary>
public interface IFilesService
{
    /// <summary>
    /// Upload file
    /// </summary>
    Task<Models.Files.FileInfo> UploadFileAsync(IFormFile file);
    
    /// <summary>
    /// Get file (for download). If fileId is incorrect - throws an exception
    /// </summary>
    Task<Models.Files.File> GetFileAsync(Guid fileId);
}