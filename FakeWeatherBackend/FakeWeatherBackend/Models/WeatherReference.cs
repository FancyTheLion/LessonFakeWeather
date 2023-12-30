using FakeWeatherBackend.Models.API.DTOs;

namespace FakeWeatherBackend.Models;

/// <summary>
/// Weather reference
/// </summary>
public class WeatherReference
{
    /// <summary>
    /// Reference timestamp
    /// </summary>
    public DateTime Timestamp { get; private set; }

    /// <summary>
    /// Reference to a weather
    /// </summary>
    public Guid WeatherId { get; private set; }

    public WeatherReference
    (
        DateTime timestamp,
        Guid weatherId
    )
    {
        Timestamp = timestamp;
        WeatherId = weatherId;
    }

    public WeatherReferenceDto ToDto()
    {
        return new WeatherReferenceDto(Timestamp, WeatherId);
    }
}