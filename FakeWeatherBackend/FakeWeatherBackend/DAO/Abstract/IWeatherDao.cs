using FakeWeatherBackend.DAO.Models;

namespace FakeWeatherBackend.DAO.Abstract;

/// <summary>
/// DAO to work with weather
/// </summary>
public interface IWeatherDao
{
    /// <summary>
    /// Get last weather
    /// </summary>
    Task<WeatherDbo> GetLastWeatherAsync();

    /// <summary>
    /// Get weather by Id
    /// </summary>
    Task<WeatherDbo> GetWeatherByIdAsync(Guid id);

    /// <summary>
    /// Get ordered weathers
    /// </summary>
    Task<IReadOnlyCollection<WeatherDbo>> GetWeathersOrderedByTimestampAsync();

    /// <summary>
    /// Add new weather to the database
    /// </summary>
    Task<WeatherDbo> AddWeatherAsync(WeatherDbo weatherToInsert);
}