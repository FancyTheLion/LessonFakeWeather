using FakeWeatherBackend.Models;

namespace FakeWeatherBackend.Services.Abstract;

public interface IWeatherService
{
    #region Get
    
    /// <summary>
    /// Get last weather references
    /// </summary>
    Task<IReadOnlyCollection<WeatherReference>> GetLastWeatherReferencesAsync();

    /// <summary>
    /// Return weather by ID
    /// </summary>
    Task<Weather> GetWeatherByIdAsync(Guid id);

    /// <summary>
    /// Return link to the latest weather
    /// </summary>
    Task<WeatherReference> GetLastWeatherReferenceAsync();
    
    #endregion

    #region Create / Update

    /// <summary>
    /// Add weather
    /// </summary>
    Task<Weather> AddWeatherAsync(Weather weatherToAdd);

    #endregion
}