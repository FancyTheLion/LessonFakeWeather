using FakeWeatherBackend.Models.API.DTOs.Users;

namespace FakeWeatherBackend.Models.Users;

/// <summary>
/// User model (corresponding to UserDbo)
/// </summary>
public class User
{
    /// <summary>
    /// User ID
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// User login
    /// </summary>
    public string UserName { get; private set; }

    public User
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

    public UserDto ToDto()
    {
        return new UserDto(Id, UserName);
    }
}