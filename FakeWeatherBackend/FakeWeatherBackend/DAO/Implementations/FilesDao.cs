using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.DAO.Models;

namespace FakeWeatherBackend.DAO.Implementations;

public class FilesDao : IFilesDao
{
    private readonly MainDbContext _dbContext;

    public FilesDao
    (
        MainDbContext dbContext
    )
    {
        _dbContext = dbContext;
    }
    
    public async Task<FileDbo> SaveFileAsync(FileDbo file)
    {
        _ = file ?? throw new ArgumentNullException(nameof(file), "File must not be null.");
        
        file.LastModifiedTime = DateTime.UtcNow;

        await _dbContext
            .Files
            .AddAsync(file);
        
        await _dbContext.SaveChangesAsync();

        return file;
    }
}