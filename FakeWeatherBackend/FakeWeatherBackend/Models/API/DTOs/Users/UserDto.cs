using System.Text.Json.Serialization;
using FakeWeatherBackend.Models.Users;

namespace FakeWeatherBackend.Models.API.DTOs.Users;

/// <summary>
/// User DTO
/// </summary>
public class UserDto
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// User login
    /// </summary>
    [JsonPropertyName("userName")]
    public string UserName { get; set; }

    public UserDto
    (
        Guid id,
        string userName
    )
    {
        Id = id;

        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentException("Username must contain something!", nameof(userName));
        }
        UserName = userName;
    }

    public User ToModel()
    {
        return new User(Id, UserName);
    }
}