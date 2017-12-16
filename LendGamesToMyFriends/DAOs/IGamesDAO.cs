using LendGamesToMyFriends.Models;
using System.Collections.Generic;

namespace LendGamesToMyFriends.DAOs
{
    public interface IGamesDAO
    {
        Game Save(Game game);

        void Update(Game game);

        IEnumerable<Game> GetAll();

        Game GetById(int id);

        IEnumerable<Game> FindByTitle(string title);

        void Remove(int id);
    }
}
