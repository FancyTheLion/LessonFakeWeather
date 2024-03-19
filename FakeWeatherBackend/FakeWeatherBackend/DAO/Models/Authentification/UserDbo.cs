using Microsoft.AspNetCore.Identity;

namespace FakeWeatherBackend.DAO.Models.Authentification;

/// <summary>
/// Site user
/// </summary>
public class UserDbo : IdentityUser<Guid>
{
    public string Name { get; set; } 
}