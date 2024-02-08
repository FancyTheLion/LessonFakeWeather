using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Models;
using FakeWeatherBackend.Models.API.DTOs;
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

    public bool ValidateWeather(WeatherDto weather)
    {
        if (weather.Temperature < -90.0 || weather.Temperature > 60.0)
        {
            return false;
        }
            
        if (weather.Cloudiness < 0.0 || weather.Cloudiness > 100.0)
        {
            return false;
        }
            
        if (weather.Humidity < 0.0 || weather.Humidity > 100.0)
        {
            return false;
        }
            
        if (weather.Pressure < 700.0 || weather.Pressure > 800.0)
        {
            return false;
        }

        return true;
    }
}