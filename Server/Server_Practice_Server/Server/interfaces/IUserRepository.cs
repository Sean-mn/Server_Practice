using Server.Models;

namespace Server.interfaces;

public interface IUserRepository
{
    User GetByUsername(string username);
}