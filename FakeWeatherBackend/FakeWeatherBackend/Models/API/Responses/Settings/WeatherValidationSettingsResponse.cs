using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs.Settings;

namespace FakeWeatherBackend.Models.API.Responses.Settings;

/// <summary>
/// Response with weather validation settings
/// </summary>
public class WeatherValidationSettingsResponse
{
    /// <summary>
    /// Single weather reference
    /// </summary>
    [JsonPropertyName("weatherValidationSettings")]
    public WeatherValidationSettingsDto WeatherValidationSettings { get; private set; }

    public WeatherValidationSettingsResponse(WeatherValidationSettingsDto weatherValidationSettings)
    {
        WeatherValidationSettings = weatherValidationSettings ??
                                    throw new ArgumentNullException(nameof(weatherValidationSettings), "Weather validation settings mustn't be null!");
    }
}