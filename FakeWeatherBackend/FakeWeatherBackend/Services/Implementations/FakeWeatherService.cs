using FakeWeatherBackend.Models;
using FakeWeatherBackend.Services.Abstract;

namespace FakeWeatherBackend.Services.Implementations;

public class FakeWeatherService : IFakeWeatherService
{
    /// <summary>
    /// Random Generation(Temperature)
    /// </summary>
    private readonly Random _randomGenerator = new Random();
    
    public async Task<Weather> GetCurrentWeatherAsync()
    {
        return new Weather
        (
            DateTime.UtcNow,
            _randomGenerator.Next(-71, 61)
        );
    }
}