using LendGamesToMyFriends.DAOs;
using LendGamesToMyFriends.Filters;
using LendGamesToMyFriends.Models;
using System;
using System.Web.Mvc;

namespace LendGamesToMyFriends.Controllers
{
    [AuthorizationFilter]
    public class GameController : Controller
    {
        private IGamesDAO dao;
        private UserReference user;

        public GameController()
        {
            dao = new GamesDAO();
        }

        // GET: Game
        public ActionResult Index(string title)
        {
            user = Session["authenticated"] as UserReference;
            var games = (string.IsNullOrEmpty(title)) ? dao.GetAll(user) : dao.FindByTitle(title, user);
            return View(games);
        }

        public ActionResult New()
        {
            user = Session["authenticated"] as UserReference;
            return View("Edit", new Game());
        }

        [HttpPost]
        public ActionResult New(Game game)
        {
            user = Session["authenticated"] as UserReference;
            try
            {
                game = dao.Save(game, user);
            }
            catch (Exception)
            {
                return View("Edit", game);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Edit(Guid? id)
        {
            user = Session["authenticated"] as UserReference;
            if (id != null)
            {
                try
                {
                    var game = dao.GetById(id.Value, user);
                    return View(game);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Não foi possível carregar o jogo. " + ex.Message;
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Game game)
        {
            user = Session["authenticated"] as UserReference;
            try
            {
                dao.Update(game, user);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Não foi possível editar o jogo. " + ex.Message;
                return View("Edit", game);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            user = Session["authenticated"] as UserReference;
            try
            {
                dao.Remove(id, user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Não foi possível remover o jogo. " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }

}