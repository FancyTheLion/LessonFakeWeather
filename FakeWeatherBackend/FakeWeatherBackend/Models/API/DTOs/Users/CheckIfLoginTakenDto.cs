using System.Text.Json.Serialization;

namespace FakeWeatherBackend.Models.API.DTOs.Users;

/// <summary>
/// DTO to check if the login is in the database
/// </summary>
public class CheckIfLoginTakenDto
{
    /// <summary>
    /// Login
    /// </summary>
    [JsonPropertyName("login")]
    public string Login { get; set; }
}