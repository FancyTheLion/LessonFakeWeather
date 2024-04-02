using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs.Users;

namespace FakeWeatherBackend.Models.API.Responses.Users;

/// <summary>
/// Response for login process
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// Login result
    /// </summary>
    [JsonPropertyName("loginResult")]
    public LoginResultDto LoginResult { get; private set; }

    public LoginResponse
    (
        LoginResultDto loginResult
    )
    {
        LoginResult = loginResult ?? throw new ArgumentNullException(nameof(loginResult), "Login result must not be null!");
    }
}