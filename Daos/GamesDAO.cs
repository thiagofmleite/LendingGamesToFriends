using LendGamesToMyFriends.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LendGamesToMyFriends.Daos
{
    public class GamesDAO
    {
        private LendingGameContext context;

        public GamesDAO()
        {
            context = new LendingGameContext();
        }

        public Game Save(Game game)
        {
            context.Games.Add(game);
            context.SaveChanges();
            return game;
        }

        public void Update(Game game)
        {
            context.Games.Update(game);
            context.SaveChanges();
        }

        public IEnumerable<Game> GetAll()
        {
            IList<Game> games = new List<Game>();
            try
            {
                games = context.Games.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return games;
        }

        public Game GetById(int id)
        {
            Game game = context.Games.FirstOrDefault(g => g.Id.Equals(id));
            return game;
        }

        public IEnumerable<Game> FindByTitle(string title)
        {
            return context.Games.Where(g => g.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        public void Remove(int id)
        {
            Game game = GetById(id);
            context.Games.Remove(game);
            context.SaveChanges();
        }
    }
}