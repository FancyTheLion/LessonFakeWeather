using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs.Users;

namespace FakeWeatherBackend.Models.API.Requests.Users;

/// <summary>
/// Request to register new user
/// </summary>
public class RegistrationRequest
{
    /// <summary>
    /// Registering user data
    /// </summary>
    [JsonPropertyName("request")]
    public RegistrationRequestDto Request { get; set; }
}