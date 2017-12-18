using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LendGamesToMyFriends.Models;
using LendGamesToMyFriends.Context;

namespace LendGamesToMyFriends.DAOs
{
    public class LendingsDAO : ILendingsDAO, IDisposable
    {
        private LendGamesContext context;

        public LendingsDAO()
        {
            context = new LendGamesContext();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IEnumerable<Lending> GetAll(UserReference user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lending> GetByFriend(Friend friend, UserReference user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lending> GetByGame(Game game, UserReference user)
        {
            throw new NotImplementedException();
        }

        public Lending GetById(Guid id, UserReference user)
        {
            throw new NotImplementedException();
        }

        public void Remove(Lending lending, UserReference user)
        {
            throw new NotImplementedException();
        }

        public Lending Save(Lending lending, UserReference user)
        {
            lending.Id = Guid.NewGuid();
            lending.User = context.Users.FirstOrDefault(u => u.Id.Equals(user.Id));
            lending.LendDate = DateTime.Now;
            lending = context.Lendings.Add(lending);
            context.SaveChanges();
            return lending;
        }

        public void Update(Lending lending, UserReference user)
        {
            throw new NotImplementedException();
        }
    }
}