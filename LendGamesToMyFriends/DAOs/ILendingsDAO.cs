using LendGamesToMyFriends.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendGamesToMyFriends.DAOs
{
    public interface ILendingsDAO
    {
        Lending Save(Lending lending, User user);
        IEnumerable<Lending> GetAll(User user);
        Lending GetById(Guid id, User user);
        IEnumerable<Lending> GetByFriend(Friend friend, User user);
        IEnumerable<Lending> GetByGame(Game game, User user);
        void Update(Lending lending, User user);
        void Remove(Lending lending, User user);
    }
}
