using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.Enums;

namespace FakeWeatherBackend.Models.API.DTOs.Users;

/// <summary>
/// DTO with user registration result
/// </summary>
public class RegistrationResultDto
{
    /// <summary>
    /// Registered user ID. Will be Guid.Empty in case of error
    /// </summary>
    [JsonPropertyName("userId")]
    public Guid UserId { get; private set; }

    /// <summary>
    /// Registration result
    /// </summary>
    [JsonPropertyName("result")]
    public RegistrationResult Result { get; private set; }

    public RegistrationResultDto
    (
        Guid userId,
        RegistrationResult result
    )
    {
        UserId = userId;
        Result = result;
    }
}