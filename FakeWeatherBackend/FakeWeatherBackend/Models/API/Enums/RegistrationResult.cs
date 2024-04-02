namespace FakeWeatherBackend.Models.API.Enums;

/// <summary>
/// Registration result
/// </summary>
public enum RegistrationResult
{
    OK = 0,
    
    LoginIsTaken = 1,
    
    GenericError = 2
}