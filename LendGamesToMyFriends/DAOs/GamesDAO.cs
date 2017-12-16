using LendGamesToMyFriends.Context;
using LendGamesToMyFriends.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace LendGamesToMyFriends.DAOs
{
    public class GamesDAO : IGamesDAO
    {
        private LendGamesContext context;

        public GamesDAO()
        {
            context = new LendGamesContext();
        }

        public Game Save(Game game, UserReference user)
        {
            game.User = user;
            game.Status = true;
            context.Games.Add(game);
            context.SaveChanges();
            return game;
        }

        public void Update(Game game, UserReference user)
        {
            try
            {
                context.Games.AddOrUpdate(game);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Game> GetAll(UserReference user)
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

        public Game GetById(int id, UserReference user)
        {
            Game game = context.Games.FirstOrDefault(g => g.Id.Equals(id));
            return game;
        }

        public IEnumerable<Game> FindByTitle(string title, UserReference user)
        {
            return context.Games.Where(g => g.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        public void Remove(int id, UserReference user)
        {
            Game game = GetById(id, user);
            context.Games.Remove(game);
            context.SaveChanges();
        }
    }
}