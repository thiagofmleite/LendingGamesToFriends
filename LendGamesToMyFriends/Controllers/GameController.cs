using LendGamesToMyFriends.DAOs;
using LendGamesToMyFriends.Models;
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
        private UserReference user;

        public GameController()
        {
            dao = new GamesDAO();
        }

        // GET: Game
        public ActionResult Index()
        {
            user = Session["authenticated"] as UserReference;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var games = dao.GetAll(user);
            return View(games);
        }

        public ActionResult New()
        {
            user = Session["authenticated"] as UserReference;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Edit");
        }

        [HttpPost]
        public ActionResult New(Game game)
        {
            user = Session["authenticated"] as UserReference;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
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


        public ActionResult Edit(int? id)
        {
            user = Session["authenticated"] as UserReference;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
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

        public ActionResult Delete(int id)
        {
            user = Session["authenticated"] as UserReference;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
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