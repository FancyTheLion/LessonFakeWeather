using System.Text.Json.Serialization;

namespace FakeWeatherBackend.Models.API.DTOs.Users;

/// <summary>
/// The result of checking the presence of a login in the database
/// </summary>
public class CheckIfLoginTakenResultDto
{
    /// <summary>
    /// Is login taken?
    /// </summary>
    [JsonPropertyName("isTaken")]
    public bool IsTaken { get; private set; }

    public CheckIfLoginTakenResultDto
    (
        bool isTaken
    )
    {
        IsTaken = isTaken;
    }
}