using LendGamesToMyFriends.Models;
using System;
using System.Collections.Generic;

namespace LendGamesToMyFriends.DAOs
{
    public interface IGamesDAO
    {
        Game Save(Game game, UserReference user);
        void Update(Game game, UserReference user);
        IEnumerable<Game> GetAll(UserReference user);
        Game GetById(Guid id, UserReference user);
        IEnumerable<Game> FindByTitle(string title, UserReference user);
        void Remove(Guid id, UserReference user);
        bool IsMyGame(Game game, UserReference user);
    }
}
