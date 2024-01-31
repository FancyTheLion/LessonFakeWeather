using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Models;
using FakeWeatherBackend.Services.Abstract;

namespace FakeWeatherBackend.Services.Implementations;

public class WeatherService : IWeatherService
{
    private readonly IWeatherDao _weatherDao;
    private readonly IWeatherMapper _weatherMapper;

    public WeatherService
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
        await AddWeatherAsync(new Weather(new Guid("734E6941-6EE6-40EC-BBC3-E2EBC3734265"),
            DateTime.UtcNow,
            25,
            47));
        
        return _weatherMapper.Map(await _weatherDao.GetWeatherByIdAsync(id));
    }

    public async Task<WeatherReference> GetLastWeatherReferenceAsync()
    {
        var lastWeather = await _weatherDao.GetLastWeatherAsync();
        
        return new WeatherReference(lastWeather.Timestamp, lastWeather.Id);
    }

    public async Task<Weather> AddWeatherAsync(Weather weatherToAdd)
    {
        _ = weatherToAdd ?? throw new ArgumentNullException(nameof(weatherToAdd), "Weather mustn't be null!");

        var weatherDbo = _weatherMapper.Map(weatherToAdd);

        weatherDbo.Id = Guid.Empty;

        return _weatherMapper.Map(await _weatherDao.AddWeatherAsync(weatherDbo));
    }
}