using FakeWeatherBackend.Models;

namespace FakeWeatherBackend.Services.Abstract;

public interface IFakeWeatherService
{
    /// <summary>
    /// Send current temperature to user
    /// </summary>
    Task<Weather> GetCurrentWeatherAsync();
}