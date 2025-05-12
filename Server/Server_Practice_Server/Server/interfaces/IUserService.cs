using Server.Models;

namespace Server.interfaces;

public interface IUserService
{
    User Authenticate(string username, string password);
}