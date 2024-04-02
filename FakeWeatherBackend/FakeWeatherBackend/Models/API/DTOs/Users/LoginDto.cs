using System.Text.Json.Serialization;

namespace FakeWeatherBackend.Models.API.DTOs.Users;

/// <summary>
/// DTO with data for user logging in
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Login
    /// </summary>
    [JsonPropertyName("login")]
    public string Login { get; set; }
    
    /// <summary>
    /// Password
    /// </summary>
    [JsonPropertyName("password")]
    public string Password { get; set; }
}