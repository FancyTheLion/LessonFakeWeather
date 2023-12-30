using FakeWeatherBackend.Models;
using FakeWeatherBackend.Services.Abstract;

namespace FakeWeatherBackend.Services.Implementations;

public class FakeWeatherService : IFakeWeatherService
{
    /// <summary>
    /// Here we store weathers. TODO: Replace it with database
    /// </summary>
    private Dictionary<Guid, Weather> _weathers = new Dictionary<Guid, Weather>()
    {
        {
            new Guid("A12694CF-9881-47A4-B2B7-05647F33C768"),
            new Weather
            (
                new Guid("A12694CF-9881-47A4-B2B7-05647F33C768"),
                new DateTime(2024, 01, 01), 
                10
            )
        },
        
        {
            new Guid("2679E2DF-B171-4068-B91D-D5A667F91700"),
            new Weather
            (
                new Guid("2679E2DF-B171-4068-B91D-D5A667F91700"),
                new DateTime(2024, 01, 02), 
                11
            )
        },
        
        {
            new Guid("B994362D-CC21-45B8-96B0-0FF662451048"),
            new Weather
            (
                new Guid("B994362D-CC21-45B8-96B0-0FF662451048"),
                new DateTime(2024, 01, 03), 
                12
            )
        },
        
        {
            new Guid("A64D0048-B1C4-4A5A-8421-833439BEAABF"),
            new Weather
            (
                new Guid("A64D0048-B1C4-4A5A-8421-833439BEAABF"),
                new DateTime(2024, 01, 04), 
                13
            )
        },
        
        {
            new Guid("63EE5CFA-3EF3-4877-8FC6-96F532378A4A"),
            new Weather
            (
                new Guid("63EE5CFA-3EF3-4877-8FC6-96F532378A4A"),
                new DateTime(2024, 01, 05), 
                14
            )
        },
        
        {
            new Guid("1B9DF8D5-604E-470C-AEBE-5AE902C7E184"),
            new Weather
            (
                new Guid("1B9DF8D5-604E-470C-AEBE-5AE902C7E184"),
                new DateTime(2024, 01, 06), 
                15
            )
        },
        
        {
            new Guid("FE731319-9248-4D7E-87DC-CA2F0692A194"),
            new Weather
            (
                new Guid("FE731319-9248-4D7E-87DC-CA2F0692A194"),
                new DateTime(2024, 01, 07), 
                16
            )
        }
    };
    
    /// <summary>
    /// Random Generation(Temperature)
    /// </summary>
    private readonly Random _randomGenerator = new Random();
    
    public async Task<Weather> GetCurrentWeatherAsync()
    {
        return new Weather
        (
            Guid.NewGuid(),
            DateTime.UtcNow,
            _randomGenerator.Next(-71, 61)
        );
    }

    public async Task<IReadOnlyCollection<WeatherReference>> GetLastWeatherReferencesAsync()
    {
        return _weathers
            .Select(kvp => kvp.Value)
            .Select(w => new WeatherReference(w.Timestamp, w.Id))
            .OrderBy(wr => wr.Timestamp)
            .ToList();
    }

    public async Task<Weather> GetWeatherById(Guid id)
    {
        return _weathers[id];
    }

    public async Task<WeatherReference> GetLastWeatherReferenceAsync()
    {
        var lastWeather = _weathers
            .Select(kvp => kvp.Value)
            .OrderBy(w => w.Timestamp)
            .Last();

        return new WeatherReference(lastWeather.Timestamp, lastWeather.Id);
    }
}