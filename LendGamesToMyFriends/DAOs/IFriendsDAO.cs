using LendGamesToMyFriends.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendGamesToMyFriends.DAOs
{
    public interface IFriendsDAO : IDisposable
    {
        Friend Save(Friend friend, UserReference user);
        IEnumerable<Friend> GetAll(UserReference user);
        Friend GetById(Guid id, UserReference user);
        IEnumerable<Friend> GetByName(string name, UserReference user);
        void Update(Friend friend, UserReference user);
        void Remove(Guid id, UserReference user);
        bool IsMyFriend(Friend friend, UserReference user);
    }
}
