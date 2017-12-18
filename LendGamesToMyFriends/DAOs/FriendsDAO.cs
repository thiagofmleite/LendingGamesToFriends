using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LendGamesToMyFriends.Models;
using LendGamesToMyFriends.Context;
using System.Data.Entity.Migrations;

namespace LendGamesToMyFriends.DAOs
{
    public class FriendsDAO : IFriendsDAO
    {
        private LendGamesContext context;

        public FriendsDAO()
        {
            context = new LendGamesContext();
        }

        public IEnumerable<Friend> GetAll(UserReference user)
        {
            return context.Friends.Where(f => f.User.Id.Equals(user.Id));
        }

        public Friend GetById(Guid id, UserReference user)
        {
            return context.Friends.FirstOrDefault(f => f.Id.Equals(id) && f.User.Id.Equals(user.Id));
        }

        public IEnumerable<Friend> GetByName(string name, UserReference user)
        {
            name = name.ToLower();
            return context.Friends.Where(f => (f.Name.ToLower().Contains(name) || f.LastName.ToLower().Contains(name)) && f.User.Id.Equals(user.Id));
        }

        public void Remove(Guid id, UserReference user)
        {
            var friend = GetById(id, user);
            context.Friends.Remove(friend);
            context.SaveChanges();
        }

        public Friend Save(Friend friend, UserReference user)
        {
            friend.Id = Guid.NewGuid();
            friend.User = user;
            friend = context.Friends.Add(friend);
            context.SaveChanges();
            return friend;
        }

        public void Update(Friend friend, UserReference user)
        {
            try
            {
                if (IsMyFriend(friend, user))
                {
                    context.Friends.AddOrUpdate(friend);
                }
                else
                {
                    throw new Exception("Amigo não encontrado");
                }
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível alterar o amigo");
            }
        }

        public bool IsMyFriend(Friend friend, UserReference user)
        {
            return context.Friends.Any(f => f.Id.Equals(friend.Id) && f.User.Id.Equals(user.Id));
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}