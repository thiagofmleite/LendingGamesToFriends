using LendGamesToMyFriends.Models;

namespace LendGamesToMyFriends.DAOs
{
    public interface IUsersDAO
    {
        User Register(User user);
        User GetUserByEmail(string email);
        bool UserExists(string email);
        UserReference Authenticate(string email, string password);
    }
}
