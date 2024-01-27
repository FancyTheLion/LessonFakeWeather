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
            .OrderBy(w => w.Timestamp)
            .LastAsync();
    }

    public async Task<WeatherDbo> GetWeatherByIdAsync(Guid id)
    {
        return await _dbContext
            .Weathers
            .SingleAsync(w => w.Id == id);
    }

    public async Task<IReadOnlyCollection<WeatherDbo>> GetWeathersOrderedByTimestampAsync()
    {
        return await _dbContext
            .Weathers
            .OrderBy(t => t.Timestamp)
            .ToListAsync();
    }
}