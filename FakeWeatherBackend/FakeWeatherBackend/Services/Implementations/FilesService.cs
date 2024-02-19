using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.Helpers;
using FakeWeatherBackend.Models.API.DTOs.Files;
using FakeWeatherBackend.Services.Abstract;
using File = FakeWeatherBackend.Models.Files.File;
using FileInfo = FakeWeatherBackend.Models.Files.FileInfo;

namespace FakeWeatherBackend.Services.Implementations;

public class FilesService : IFilesService
{
    private readonly IFilesDao _filesDao;

    public FilesService
    (
        IFilesDao filesDao
    )
    {
        _filesDao = filesDao;
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

        return new File()
        {
            Id = fileFromDb.Id,
            Content = fileFromDb.Content,
            Name = fileFromDb.Name,
            Type = fileFromDb.Type,
            LastModifiedTime = fileFromDb.LastModifiedTime,
            Hash = fileFromDb.Hash
        };
    }
}