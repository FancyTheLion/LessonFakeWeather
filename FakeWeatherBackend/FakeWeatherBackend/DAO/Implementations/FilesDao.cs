using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.DAO.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<FileDbo> GetFileAsync(Guid fileId)
    {
        return await _dbContext
            .Files
            .SingleAsync(f => f.Id == fileId);
    }
}