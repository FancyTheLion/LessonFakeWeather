using FakeWeatherBackend.Models.API.DTOs.Users;

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
}