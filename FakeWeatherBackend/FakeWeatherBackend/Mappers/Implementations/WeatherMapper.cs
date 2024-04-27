using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.DAO.Models.Comments;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Models;
using File = FakeWeatherBackend.Models.Files.File;

namespace FakeWeatherBackend.Mappers.Implementations;

public class WeatherMapper : IWeatherMapper
{
    private readonly IFilesMapper _filesMapper;
    private readonly ICommentsMapper _commentsMapper;

    public WeatherMapper
    (
        IFilesMapper filesMapper,
        ICommentsMapper commentsMapper
    )
    {
        _filesMapper = filesMapper;
        _commentsMapper = commentsMapper;
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
            _filesMapper.Map(weather.PhotoPreview),
            _commentsMapper.Map(weather.Comments)
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
            PhotoPreview = _filesMapper.Map(weather.PhotoPreview),
            Comments = new List<CommentDbo>() // Load it in DAO
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