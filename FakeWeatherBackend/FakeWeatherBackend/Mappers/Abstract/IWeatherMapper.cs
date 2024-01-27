using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.Models;

namespace FakeWeatherBackend.Mappers.Abstract;

public interface IWeatherMapper
{
    /// <summary>
    /// Take the weather that came from the database and make it into the weather that the rest of the program uses
    /// </summary>
    Weather Map(WeatherDbo weather);
    
    /// <summary>
    /// Multiple weathers DBOs to list of models
    /// </summary>
    IList<Weather> Map(IReadOnlyCollection<WeatherDbo> weathers);
    
    /// <summary>
    /// Take the weather that the program uses and make it into weather that can be put into the database
    /// </summary>
    WeatherDbo Map(Weather weather);

    /// <summary>
    /// Multiple weather models to list of DBOs
    /// </summary>
    IList<WeatherDbo> Map(IReadOnlyCollection<Weather> weathers);
}