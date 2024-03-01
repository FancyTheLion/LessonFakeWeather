using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.Helpers;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Models.API.DTOs.Files;
using FakeWeatherBackend.Models.Files;
using FakeWeatherBackend.Services.Abstract;
using File = FakeWeatherBackend.Models.Files.File;
using FileInfo = FakeWeatherBackend.Models.Files.FileInfo;

namespace FakeWeatherBackend.Services.Implementations;

public class FilesService : IFilesService
{
    /// <summary>
    /// Maximal allowed file size
    /// </summary>
    private const int MaxAllowedFileSize = 10 * 1024 * 1024;
    
    /// <summary>
    /// Allowed images MIME types
    /// </summary>
    private readonly IReadOnlyCollection<string> _allowedImagesTypes = new List<string>()
    {
        "image/jpeg", // JPEG
        "image/png", // PNG
        "image/gif", // GIF
        "image/webp" // WEBP    
    };
    
    private readonly IFilesDao _filesDao;
    
    private readonly IFilesMapper _filesMapper;

    public FilesService
    (
        IFilesDao filesDao,
        IFilesMapper filesMapper
    )
    {
        _filesDao = filesDao;
        _filesMapper = filesMapper;
    }
    
    public async Task<Models.Files.FileInfo> UploadFileAsync(IFormFile file)
    {
        _ = file ?? throw new ArgumentNullException(nameof(file), "File must not be null!");

        var content = new byte[file.Length];
        using (var fileStream = file.OpenReadStream())
        {
            fileStream.Read(content, 0, (int)file.Length);
        }

        var fileDbo = new FileDbo()
        {
            Name = file.FileName,
            Type = file.ContentType,
            Content = content,
            Hash = SHA512Helper.CalculateSHA512(content)
        };

        var savedFile = await _filesDao.SaveFileAsync(fileDbo);

        return new FileInfo(savedFile.Id, savedFile.Name);
    }
    
    public async Task<File> GetFileAsync(Guid fileId)
    {
        var fileFromDb = await _filesDao.GetFileAsync(fileId);

        return _filesMapper.Map(fileFromDb);
    }

    public async Task<bool> IsFileImageAsync(IFormFile file)
    {
        // TODO: Check file content, not a type!
        if (_allowedImagesTypes.Contains(file.ContentType))
        {
            return true;
        }
        
        return false;
    }

    public async Task<bool> IsFileSizeCorrectAsync(IFormFile file)
    {
        if (file.Length > MaxAllowedFileSize)
        {
            return false;
        }

        return true;
    }
}