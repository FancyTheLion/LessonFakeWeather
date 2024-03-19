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
    /// Upload file (overload)
    /// </summary>
    Task<Models.Files.FileInfo> UploadFileAsync
    (
        string name,
        string type,
        byte[] content
    );
    
    /// <summary>
    /// Get file (for download). If fileId is incorrect - throws an exception
    /// </summary>
    Task<Models.Files.File> GetFileAsync(Guid fileId);
    
    #region Validation

    /// <summary>
    /// Is given file image or not?
    /// </summary>
    Task<bool> IsFileImageAsync(IFormFile file);

    /// <summary>
    /// Is given file size is allowed to upload?
    /// </summary>
    Task<bool> IsFileSizeCorrectAsync(IFormFile file);

    #endregion
}