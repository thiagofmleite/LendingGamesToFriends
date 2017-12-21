using LendGamesToMyFriends.Models;
using System;
using System.Collections.Generic;

namespace LendGamesToMyFriends.DAOs
{
    public interface ILendingsDAO : IDisposable
    {
        Lending Save(Lending lending, UserReference user);
        IEnumerable<Lending> GetAll(UserReference user);
        Lending GetById(Guid id, UserReference user);
        IEnumerable<Lending> GetByFriend(Friend friend, UserReference user);
        IEnumerable<Lending> GetByGame(Game game, UserReference user);
        void Update(Lending lending, UserReference user);
        void Remove(Lending lending, UserReference user);
    }
}
