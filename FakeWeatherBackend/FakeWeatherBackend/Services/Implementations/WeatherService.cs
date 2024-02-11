using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Models;
using FakeWeatherBackend.Models.API.DTOs;
using FakeWeatherBackend.Models.Settings;
using FakeWeatherBackend.Services.Abstract;
using Microsoft.Extensions.Options;

namespace FakeWeatherBackend.Services.Implementations;

public class WeatherService : IWeatherService
{
    private readonly IWeatherDao _weatherDao;
    private readonly IWeatherMapper _weatherMapper;

    private readonly WeatherValidationSettings _weatherValidationSettings;

    public WeatherService
    (
        IWeatherDao weatherDao,
        IWeatherMapper weatherMapper,
        IOptions<WeatherValidationSettings> weatherValidationSettingsRef)
    {
        _weatherDao = weatherDao;
        _weatherMapper = weatherMapper;

        _weatherValidationSettings = weatherValidationSettingsRef.Value;
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
        if
        (
            weather.Temperature < _weatherValidationSettings.LowestPossibleTemperature
            ||
            weather.Temperature > _weatherValidationSettings.HightestPossibleTemperature
        )
        {
            return false;
        }
            
        if
        (
            weather.Cloudiness < _weatherValidationSettings.LowestPossibleCloudiness
            ||
            weather.Cloudiness > _weatherValidationSettings.HightestPossibleCloudiness
        )
        {
            return false;
        }
            
        if
        (
            weather.Humidity < _weatherValidationSettings.LowestPossibleHumidity
            ||
            weather.Humidity > _weatherValidationSettings.HightestPossibleHumidity
        )
        {
            return false;
        }
            
        if
        (
            weather.Pressure < _weatherValidationSettings.LowestPossiblePressure
            ||
            weather.Pressure > _weatherValidationSettings.HightestPossiblePressure
        )
        {
            return false;
        }

        return true;
    }
}