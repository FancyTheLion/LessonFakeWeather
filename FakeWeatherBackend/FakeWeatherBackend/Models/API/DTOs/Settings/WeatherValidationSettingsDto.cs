using System.Text.Json.Serialization;

namespace FakeWeatherBackend.Models.API.DTOs.Settings;

/// <summary>
/// Weather validation settings DTO
/// </summary>
public class WeatherValidationSettingsDto
{
    #region Temperature

    /// <summary>
    /// Lowest acceptable temperature
    /// </summary>
    [JsonPropertyName("lowestPossibleTemperature")]
    public double LowestPossibleTemperature { get; private set; }
    
    /// <summary>
    /// Highest acceptable temperature
    /// </summary>
    [JsonPropertyName("highestPossibleTemperature")]
    public double HighestPossibleTemperature { get; private set; }

    #endregion
    
    #region Cloudiness
    
    /// <summary>
    /// Lowest acceptable cloudiness
    /// </summary>
    [JsonPropertyName("lowestPossibleCloudiness")]
    public double LowestPossibleCloudiness { get; private set; }
    
    /// <summary>
    /// Highest acceptable cloudiness
    /// </summary>
    [JsonPropertyName("highestPossibleCloudiness")]
    public double HighestPossibleCloudiness { get; private set; }
    
    #endregion
    
    #region Humidity
    
    /// <summary>
    /// Lowest acceptable humidity
    /// </summary>
    [JsonPropertyName("lowestPossibleHumidity")]
    public double LowestPossibleHumidity { get; private set; }
    
    /// <summary>
    /// Highest acceptable humidity
    /// </summary>
    [JsonPropertyName("highestPossibleHumidity")]
    public double HighestPossibleHumidity { get; private set; }
    
    #endregion
    
    #region Pressure
    
    /// <summary>
    /// Lowest acceptable pressure
    /// </summary>
    [JsonPropertyName("lowestPossiblePressure")]
    public double LowestPossiblePressure { get; private set; }
    
    /// <summary>
    /// Highest acceptable pressure
    /// </summary>
    [JsonPropertyName("highestPossiblePressure")]
    public double HighestPossiblePressure { get; private set; }
    
    #endregion

    public WeatherValidationSettingsDto
    (
        double lowestPossibleTemperature,
        double highestPossibleTemperature,
        double lowestPossibleCloudiness,
        double highestPossibleCloudiness,
        double lowestPossibleHumidity,
        double highestPossibleHumidity,
        double lowestPossiblePressure,
        double highestPossiblePressure
    )
    {
        LowestPossibleTemperature = lowestPossibleTemperature;
        HighestPossibleTemperature = highestPossibleTemperature;
        LowestPossibleCloudiness = lowestPossibleCloudiness;
        HighestPossibleCloudiness = highestPossibleCloudiness;
        LowestPossibleHumidity = lowestPossibleHumidity;
        HighestPossibleHumidity = highestPossibleHumidity;
        LowestPossiblePressure = lowestPossiblePressure;
        HighestPossiblePressure = highestPossiblePressure;
    }
}