using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs.Users;

namespace FakeWeatherBackend.Models.API.Requests.Users;

/// <summary>
/// Request to check if the login is busy
/// </summary>
public class CheckIfLoginTakenRequest
{
    /// <summary>
    /// Check if the login is busy
    /// </summary>
    [JsonPropertyName("checkData")]
    public CheckIfLoginTakenDto CheckData { get; set; }
}