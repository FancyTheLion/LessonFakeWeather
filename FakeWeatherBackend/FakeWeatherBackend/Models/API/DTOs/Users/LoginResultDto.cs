using System.Text.Json.Serialization;

namespace FakeWeatherBackend.Models.API.DTOs.Users;

/// <summary>
/// Result of login process
/// </summary>
public class LoginResultDto
{
    /// <summary>
    /// Is credentials correct?
    /// </summary>
    [JsonPropertyName("isSuccessful")]
    public bool IsSuccessful { get; }

    /// <summary>
    /// Token
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; }

    /// <summary>
    /// Token expiration date and time
    /// </summary>
    [JsonPropertyName("expiration")]
    public DateTime ExpirationTime { get; }
    
    public LoginResultDto
    (
        bool isSuccessful,
        string token,
        DateTime expirationTime
    )
    {
        IsSuccessful = isSuccessful;
        Token = token; // May be empty if login unsuccessfull
        ExpirationTime = expirationTime;
    }
}

