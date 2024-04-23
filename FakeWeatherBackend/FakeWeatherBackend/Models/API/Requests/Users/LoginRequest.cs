using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs.Users;

namespace FakeWeatherBackend.Models.API.Requests.Users;

/// <summary>
/// Request for log-in
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Login data
    /// </summary>
    [JsonPropertyName("loginData")]
    public LoginDto LoginData { get; set; }
}