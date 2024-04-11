using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.API.DTOs.Users;

namespace FakeWeatherBackend.Models.API.Responses.Users;


/// <summary>
/// Result of the response to a login busy request
/// </summary>
public class CheckIfLoginTakenResponse
{
    /// <summary>
    /// Check result
    /// </summary>
    [JsonPropertyName("checkResult")]
    public CheckIfLoginTakenResultDto CheckResult { get; private set; }

    public CheckIfLoginTakenResponse
    (
        CheckIfLoginTakenResultDto checkResult
    )
    {
        CheckResult = checkResult ?? throw new ArgumentNullException(nameof(checkResult), "Check result must not be null!");
    }
}