using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Models;
using FakeWeatherBackend.Services.Abstract;

namespace FakeWeatherBackend.Services.Implementations;

public class FakeWeatherService : IFakeWeatherService
{
    private readonly IWeatherDao _weatherDao;
    private readonly IWeatherMapper _weatherMapper;

    public FakeWeatherService
    (
        IWeatherDao weatherDao,
        IWeatherMapper weatherMapper
    )
    {
        _weatherDao = weatherDao;
        _weatherMapper = weatherMapper;
    }
    
    public async Task<IReadOnlyCollection<WeatherReference>> GetLastWeatherReferencesAsync()
    {
        return (await _weatherDao
                .GetWeathersOrderedByTimestampAsync())
            .Select(w => new WeatherReference(w.Timestamp, w.Id))
            .ToList();
    }

    public async Task<Weather> GetWeatherByIdAsync(Guid id)
    {
        return _weatherMapper.Map(await _weatherDao.GetWeatherByIdAsync(id));
    }

    public async Task<WeatherReference> GetLastWeatherReferenceAsync()
    {
        var lastWeather = await _weatherDao.GetLastWeatherAsync();
        
        return new WeatherReference(lastWeather.Timestamp, lastWeather.Id);
    }
}