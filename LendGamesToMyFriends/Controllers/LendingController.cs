using LendGamesToMyFriends.DAOs;
using LendGamesToMyFriends.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LendGamesToMyFriends.Controllers
{
    public class LendingController : Controller
    {
        private ILendingsDAO dao;
        private IFriendsDAO friendsDao;
        private IGamesDAO gamesDao;
        private UserReference user;

        public LendingController()
        {
            dao = new LendingsDAO();
            friendsDao = new FriendsDAO();
            gamesDao = new GamesDAO();
        }

        // GET: Lending
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            user = Session["authenticated"] as UserReference;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Friends = friendsDao.GetAll(user).Select(f => new SelectListItem { Text = $"{f.Name} {f.LastName}", Value = f.Id.ToString() }).ToList();
            ViewBag.Games = gamesDao.GetAll(user).Select(g => new SelectListItem { Text = g.Title, Value = g.Id.ToString(), Disabled = g.Status }).ToList();
            return View(new Lending());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(Guid friendId, Guid gameId)
        {
            user = Session["authenticated"] as UserReference;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    Game game = gamesDao.GetById(gameId, user);
                    Friend friend = friendsDao.GetById(friendId, user);
                    Lending lending = new Lending
                    {
                        Friend = friend,
                        Game = game
                    };
                    dao.Save(lending, user);
                    game.Status = false;
                    gamesDao.Update(game, user);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Não foi possível emprestar o jogo: " + ex.Message;
                    return View();
                }
            }
            else
            {
                ViewBag.Error = "Selecione o amigo e o jogo";
                return View();
            }
        }
    }
}