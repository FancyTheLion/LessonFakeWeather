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
    private readonly IFilesService _filesService;
    private readonly IResizeService _resizeService;
    
    private readonly WeatherValidationSettings _weatherValidationSettings;

    public WeatherService
    (
        IWeatherDao weatherDao,
        IWeatherMapper weatherMapper,
        IOptions<WeatherValidationSettings> weatherValidationSettingsRef,
        IFilesService filesService, 
        IResizeService resizeService
    )
    {
        _weatherDao = weatherDao;
        _weatherMapper = weatherMapper;
        _weatherValidationSettings = weatherValidationSettingsRef.Value;
        _filesService = filesService;
        _resizeService = resizeService;
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

        if (lastWeather == null)
        {
            return null;
        }
        
        return new WeatherReference(lastWeather.Timestamp, lastWeather.Id);
    }

    public async Task<Weather> AddWeatherAsync(Weather weatherToAdd)
    {
        _ = weatherToAdd ?? throw new ArgumentNullException(nameof(weatherToAdd), "Weather mustn't be null!");

        // Getting weather photo
        var weatherPhoto = await _filesService.GetFileAsync(weatherToAdd.Photo.Id);

        // Resized photo
        var resizedPhotoName = Path.GetFileNameWithoutExtension(weatherPhoto.Name)
                               + "_preview"
                               + Path.GetExtension(weatherPhoto.Name);
        
        var weatherPhotoPreview = await _filesService.UploadFileAsync
        (
            resizedPhotoName,
            weatherPhoto.Type,
            await _resizeService.ResizeImageAsync(weatherPhoto.Content, weatherPhoto.Type)
        );

        weatherToAdd.PhotoPreview.Id = weatherPhotoPreview.Id;
        
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
            weather.Temperature > _weatherValidationSettings.HighestPossibleTemperature
        )
        {
            return false;
        }
            
        if
        (
            weather.Cloudiness < _weatherValidationSettings.LowestPossibleCloudiness
            ||
            weather.Cloudiness > _weatherValidationSettings.HighestPossibleCloudiness
        )
        {
            return false;
        }
            
        if
        (
            weather.Humidity < _weatherValidationSettings.LowestPossibleHumidity
            ||
            weather.Humidity > _weatherValidationSettings.HighestPossibleHumidity
        )
        {
            return false;
        }
            
        if
        (
            weather.Pressure < _weatherValidationSettings.LowestPossiblePressure
            ||
            weather.Pressure > _weatherValidationSettings.HighestPossiblePressure
        )
        {
            return false;
        }

        return true;
    }

    public async Task<WeatherValidationSettings> GetWeatherValidationSettingsAsync()
    {
        return _weatherValidationSettings;
    }
}