using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Models;
using File = FakeWeatherBackend.Models.Files.File;

namespace FakeWeatherBackend.Mappers.Implementations;

public class WeatherMapper : IWeatherMapper
{
    private readonly IFilesMapper _filesMapper;

    public WeatherMapper
    (
        IFilesMapper filesMapper
    )
    {
        _filesMapper = filesMapper;
    }
    
    public Weather Map(WeatherDbo weather)
    {
        if (weather == null)
        {
            return null;
        }
        
        return new Weather
        (
            weather.Id,
            weather.Timestamp,
            weather.Temperature,
            weather.Cloudiness,
            weather.Humidity,
            weather.Pressure,
            _filesMapper.Map(weather.Photo),
            _filesMapper.Map(weather.PhotoPreview)
        );
    }

    public IList<Weather> Map(IReadOnlyCollection<WeatherDbo> weathers)
    {
        if (weathers == null)
        {
            return null;
        }

        return weathers
            .Select(w => Map(w))
            .ToList();
    }

    public WeatherDbo Map(Weather weather)
    {
        if (weather == null)
        {
            return null;
        }
        
        return new WeatherDbo()
        {
            Id = weather.Id,
            Timestamp = weather.Timestamp,
            Temperature = weather.Temperature,
            Cloudiness = weather.Cloudiness,
            Humidity = weather.Humidity,
            Pressure = weather.Pressure,
            Photo = _filesMapper.Map(weather.Photo),
            PhotoPreview = _filesMapper.Map(weather.PhotoPreview)
        };
    }

    public IList<WeatherDbo> Map(IReadOnlyCollection<Weather> weathers)
    {
        if (weathers == null)
        {
            return null;
        }

        return weathers
            .Select(w => Map(w))
            .ToList();
    }
}