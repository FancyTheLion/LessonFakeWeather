using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeWeatherBackend.DAO.Implementations;

public class WeatherDao : IWeatherDao
{
    private readonly MainDbContext _dbContext;

    public WeatherDao
    (
        MainDbContext dbContext
    )
    {
        _dbContext = dbContext;
    }
    
    public async Task<WeatherDbo> GetLastWeatherAsync()
    {
        return await _dbContext
            .Weathers
            
            .Include(w => w.Photo)
                
            .OrderBy(w => w.Timestamp)
            .LastOrDefaultAsync();
    }

    public async Task<WeatherDbo> GetWeatherByIdAsync(Guid id)
    {
        return await _dbContext
            .Weathers
                
            .Include(w => w.Photo)    
                
            .SingleAsync(w => w.Id == id);
    }

    public async Task<IReadOnlyCollection<WeatherDbo>> GetWeathersOrderedByTimestampAsync()
    {
        return await _dbContext
            .Weathers
                
            .Include(w => w.Photo)    
                
            .OrderBy(t => t.Timestamp)
            .ToListAsync();
    }

    public async Task<WeatherDbo> AddWeatherAsync(WeatherDbo weatherToInsert)
    {
        _ = weatherToInsert ?? throw new ArgumentNullException(nameof(weatherToInsert), "Weather can't be null!");

        weatherToInsert.Photo = await _dbContext.Files.SingleAsync(f => f.Id == weatherToInsert.Photo.Id);
        
        await _dbContext
            .Weathers
            .AddAsync(weatherToInsert);
            
        await _dbContext.SaveChangesAsync();

        return weatherToInsert;
    }
}