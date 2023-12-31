using FakeWeatherBackend.Models;

namespace FakeWeatherBackend.Services.Abstract;

public interface IFakeWeatherService
{
    /// <summary>
    /// Get last weather references
    /// </summary>
    Task<IReadOnlyCollection<WeatherReference>> GetLastWeatherReferencesAsync();

    /// <summary>
    /// Return weather by ID
    /// </summary>
    Task<Weather> GetWeatherById(Guid id);

    /// <summary>
    /// Return link to the latest weather
    /// </summary>
    Task<WeatherReference> GetLastWeatherReferenceAsync();
}