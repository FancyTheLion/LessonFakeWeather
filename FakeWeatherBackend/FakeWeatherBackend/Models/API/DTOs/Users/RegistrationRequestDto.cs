using System.Text.Json.Serialization;

namespace FakeWeatherBackend.Models.API.DTOs.Users;

/// <summary>
/// DTO with information on registering user
/// </summary>
public class RegistrationRequestDto
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