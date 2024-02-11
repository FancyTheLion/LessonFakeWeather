namespace FakeWeatherBackend.Models.Settings;

/// <summary>
/// Weather validation settings
/// </summary>
public class WeatherValidationSettings
{
    #region Temperature

    /// <summary>
    /// Lowest acceptable temperature
    /// </summary>
    public double LowestPossibleTemperature { get; set; }
    
    /// <summary>
    /// Highest acceptable temperature
    /// </summary>
    public double HightestPossibleTemperature { get; set; }

    #endregion
    
    #region Cloudiness
    
    /// <summary>
    /// Lowest acceptable cloudiness
    /// </summary>
    public double LowestPossibleCloudiness { get; set; }
    
    /// <summary>
    /// Highest acceptable cloudiness
    /// </summary>
    public double HightestPossibleCloudiness { get; set; }
    
    #endregion
    
    #region Humidity
    
    /// <summary>
    /// Lowest acceptable humidity
    /// </summary>
    public double LowestPossibleHumidity { get; set; }
    
    /// <summary>
    /// Highest acceptable humidity
    /// </summary>
    public double HightestPossibleHumidity { get; set; }
    
    #endregion
    
    #region Pressure
    
    /// <summary>
    /// Lowest acceptable pressure
    /// </summary>
    public double LowestPossiblePressure { get; set; }
    
    /// <summary>
    /// Highest acceptable pressure
    /// </summary>
    public double HightestPossiblePressure { get; set; }
    
    #endregion
}