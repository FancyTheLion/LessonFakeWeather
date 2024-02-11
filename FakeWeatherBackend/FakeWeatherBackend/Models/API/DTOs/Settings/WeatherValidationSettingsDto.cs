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
    [JsonPropertyName("hightestPossibleTemperature")]
    public double HightestPossibleTemperature { get; private set; }

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
    [JsonPropertyName("hightestPossibleCloudiness")]
    public double HightestPossibleCloudiness { get; private set; }
    
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
    [JsonPropertyName("hightestPossibleHumidity")]
    public double HightestPossibleHumidity { get; private set; }
    
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
    [JsonPropertyName("hightestPossiblePressure")]
    public double HightestPossiblePressure { get; private set; }
    
    #endregion

    public WeatherValidationSettingsDto
    (
        double lowestPossibleTemperature,
        double hightestPossibleTemperature,
        double lowestPossibleCloudiness,
        double hightestPossibleCloudiness,
        double lowestPossibleHumidity,
        double hightestPossibleHumidity,
        double lowestPossiblePressure,
        double hightestPossiblePressure
    )
    {
        LowestPossibleTemperature = lowestPossibleTemperature;
        HightestPossibleTemperature = hightestPossibleTemperature;
        LowestPossibleCloudiness = lowestPossibleCloudiness;
        HightestPossibleCloudiness = hightestPossibleCloudiness;
        LowestPossibleHumidity = lowestPossibleHumidity;
        HightestPossibleHumidity = hightestPossibleHumidity;
        LowestPossiblePressure = lowestPossiblePressure;
        HightestPossiblePressure = hightestPossiblePressure;
    }
}