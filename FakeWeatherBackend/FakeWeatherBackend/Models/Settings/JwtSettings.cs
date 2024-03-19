namespace FakeWeatherBackend.Models.Settings;

/// <summary>
/// Appsettings.json JWT settings section
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Valid audience
    /// </summary>
    public string ValidAudience { get; set; }
    
    /// <summary>
    /// Valid issuer
    /// </summary>
    public string ValidIssuer { get; set; }

    /// <summary>
    /// Secret key
    /// </summary>
    public string Secret { get; set; }

    /// <summary>
    /// Token lifetime
    /// </summary>
    public int Lifetime { get; set; }
}