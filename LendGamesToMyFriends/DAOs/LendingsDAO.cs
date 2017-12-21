using System;
using System.Collections.Generic;
using System.Linq;
using LendGamesToMyFriends.Models;
using LendGamesToMyFriends.Context;
using System.Data.Entity.Migrations;

namespace LendGamesToMyFriends.DAOs
{
    public class LendingsDAO : ILendingsDAO
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
            return context.Lendings.Where(l => l.User.Id.Equals(user.Id)).ToList();
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
            return context.Lendings.FirstOrDefault(l => l.Id.Equals(id) && l.User.Id.Equals(user.Id));
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
            if (context.Lendings.Any(l => l.Id.Equals(lending.Id) && l.User.Id.Equals(user.Id)))
            {
                lending.User = context.Users.FirstOrDefault(u => u.Id.Equals(user.Id));
                lending.ReturnDate = DateTime.Now;
                lending.Status = true;
                context.Lendings.AddOrUpdate(lending);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Esse registro não está em seu nome");
            }
        }
    }
}