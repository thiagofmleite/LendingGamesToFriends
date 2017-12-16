using LendGamesToMyFriends.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LendGamesToMyFriends.Daos
{
    public class FriendsDAO
    {
        private LendingGameContext context;

        public FriendsDAO()
        {
            context = new LendingGameContext();
        }

        public Friend Save(Friend friend)
        {
            context.Friends.Add(friend);
            context.SaveChanges();
            return friend;
        }

        public void Update(Friend friend)
        {
            context.Friends.Update(friend);
            context.SaveChanges();
        }

        public IEnumerable<Friend> GetAll()
        {
            IList<Friend> friends = new List<Friend>();
            try
            {
                friends = context.Friends.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return friends;
        }

        public Friend GetById(int id)
        {
            Friend friend = context.Friends.FirstOrDefault(g => g.Id.Equals(id));
            return friend;
        }

        public IEnumerable<Friend> FindByName(string name)
        {
            return context.Friends.Where(f => f.Name.ToLower().Contains(name.ToLower()) || f.LastName.ToLower().Contains(name.ToLower()) || f.NickName.ToLower().Contains(name.ToLower())).ToList();
        }

        public void Remove(int id)
        {
            Friend friend = GetById(id);
            context.Friends.Remove(friend);
            context.SaveChanges();
        }
    }
}