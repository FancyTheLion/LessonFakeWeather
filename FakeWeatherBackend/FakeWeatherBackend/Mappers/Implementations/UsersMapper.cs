using FakeWeatherBackend.DAO.Models.Authentification;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Models.Users;

namespace FakeWeatherBackend.Mappers.Implementations;

public class UsersMapper : IUsersMapper
{
    public User Map(UserDbo user)
    {
        if (user == null)
        {
            return null;
        }
        
        return new User(user.Id, user.UserName);
    }

    public IList<User> Map(IReadOnlyCollection<UserDbo> users)
    {
        if (users == null)
        {
            return null;
        }
        
        return users.Select(u => Map(u)).ToList();
    }

    public UserDbo Map(User user)
    {
        if (user == null)
        {
            return null;
        }
        
        return new UserDbo() { Id = user.Id };
    }

    public IList<UserDbo> Map(IReadOnlyCollection<User> users)
    {
        if (users == null)
        {
            return null;
        }
        
        return users.Select(u => Map(u)).ToList();
    }
}