using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.DAO.Models.Authentification;
using FakeWeatherBackend.Models.Users;

namespace FakeWeatherBackend.Mappers.Abstract;

/// <summary>
/// Users mapper
/// </summary>
public interface IUsersMapper
{
    User Map(UserDbo user);
    
    IList<User> Map(IReadOnlyCollection<UserDbo> users);
    
    UserDbo Map(User user);
    
    IList<UserDbo> Map(IReadOnlyCollection<User> users);
}