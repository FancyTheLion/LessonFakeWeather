using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Models;

namespace FakeWeatherBackend.Mappers.Implementations;

public class WeatherMapper : IWeatherMapper
{
    public Weather Map(WeatherDbo weather)
    {
        if (weather == null)
        {
            return null;
        }
        
        return new Weather(weather.Id, weather.Timestamp, weather.Temperature, weather.Cloudiness);
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
            Cloudiness = weather.Cloudiness
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