using FakeWeatherBackend.Services.Abstract;

namespace FakeWeatherBackend.Services.Implementations;

public class FilesService : IFilesService
{
    public Task<Models.Files.FileInfo> UploadFileAsync(IFormFile file)
    {
        throw new NotImplementedException("Method not implemented in FileService!");
    }
}