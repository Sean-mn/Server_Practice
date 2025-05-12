using Server.interfaces;
using Server.Models;

namespace Server.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User Authenticate(string username, string password)
    {
        var user = _userRepository.GetByUsername(username);
        
        if (user == null || user.Password != password)
            return null;
        
        return user;
    }
}