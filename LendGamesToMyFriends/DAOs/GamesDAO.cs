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
            game.Id = Guid.NewGuid();
            game.User = context.Users.FirstOrDefault(u => u.Id.Equals(user.Id));
            game.Status = true;
            context.Games.Add(game);
            context.SaveChanges();
            return game;
        }

        public void Update(Game game, UserReference user)
        {
            try
            {
                if (IsMyGame(game, user))
                {
                    game.User = context.Users.FirstOrDefault(u => u.Id.Equals(user.Id));
                    context.Games.AddOrUpdate(game);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Jogo não encontrado");
                }
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

        public Game GetById(Guid id, UserReference user)
        {
            Game game = context.Games.FirstOrDefault(g => g.Id.Equals(id));
            if(IsMyGame(game, user))
            {
                return game;
            }
            else
            {
                throw new Exception("Este jogo não é seu");
            }

        }

        public IEnumerable<Game> FindByTitle(string title, UserReference user)
        {
            return context.Games.Where(g => g.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        public void Remove(Guid id, UserReference user)
        {
            Game game = GetById(id, user);
            context.Games.Remove(game);
            context.SaveChanges();
        }

        public bool IsMyGame(Game game, UserReference user)
        {
            return context.Games.Any(g => g.Id.Equals(game.Id) && g.User.Id.Equals(user.Id));
        }
    }
}