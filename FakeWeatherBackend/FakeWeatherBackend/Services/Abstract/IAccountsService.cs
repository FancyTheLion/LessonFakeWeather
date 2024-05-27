using FakeWeatherBackend.Models.API.DTOs.Users;
using FakeWeatherBackend.Models.Users;

namespace FakeWeatherBackend.Services.Abstract;

/// <summary>
/// Service to work with user accounts
/// </summary>
public interface IAccountsService
{
    /// <summary>
    /// Method for user registration
    /// </summary>
    Task<RegistrationResultDto> RegisterUserAsync(RegistrationRequestDto request);
    
    /// <summary>
    /// Try to log-in user
    /// </summary>
    Task<LoginResultDto> LoginAsync(LoginDto loginData);
    
    /// <summary>
    /// Is user with given login exists?
    /// </summary>
    Task<bool> IsUserExistByLoginAsync(string login);

    /// <summary>
    /// Get user by login. Returns null if user doesn't exist
    /// </summary>
    Task<User> GetUserByLoginAsync(string login);
}