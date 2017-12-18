using LendGamesToMyFriends.Models;
using System;

namespace LendGamesToMyFriends.DAOs
{
    public interface IUsersDAO : IDisposable
    {
        User Register(User user);
        User GetUserByEmail(string email);
        bool UserExists(string email);
        UserReference Authenticate(Login login);
    }
}
