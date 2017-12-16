using LendGamesToMyFriends.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LendGamesToMyFriends.Controllers
{
    public class GameController : Controller
    {
        private GamesDAO dao;

        public GameController()
        {
            dao = new GamesDAO();
        }

        // GET: Game
        public ActionResult Index()
        {
            var games = dao.GetAll();
            return View(games);
        }

        
    }
}