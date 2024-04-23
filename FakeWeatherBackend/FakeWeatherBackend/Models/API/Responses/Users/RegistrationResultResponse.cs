using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs.Users;

namespace FakeWeatherBackend.Models.API.Responses.Users;

/// <summary>
/// Response with user registration result
/// </summary>
public class RegistrationResultResponse
{
    /// <summary>
    /// DTO with registration result
    /// </summary>
    [JsonPropertyName("registrationResult")]
    public RegistrationResultDto RegistrationResult { get; private set; }

    public RegistrationResultResponse
    (
        RegistrationResultDto registrationResult
    )
    {
        RegistrationResult = registrationResult ??
                             throw new ArgumentNullException(nameof(registrationResult),
                                 "Registration result must not be null!");
    }
}