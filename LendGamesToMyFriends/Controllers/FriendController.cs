using LendGamesToMyFriends.DAOs;
using LendGamesToMyFriends.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LendGamesToMyFriends.Controllers
{
    public class FriendController : Controller
    {
        private IFriendsDAO dao;
        private UserReference user;

        public FriendController()
        {
            dao = new FriendsDAO();
        }

        // GET: Friend
        public ActionResult Index()
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"].ToString();
                TempData.Remove("Error");
            }
            try
            {
                user = Session["authenticated"] as UserReference;
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                var friends = dao.GetAll(user);
                return View(friends);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Não foi possível listar os amigos: " + ex.Message;
                return View();
            }
        }

        public ActionResult New()
        {
            user = Session["authenticated"] as UserReference;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Edit", new Friend());
        }

        [HttpPost]
        public ActionResult New(Friend friend)
        {
            user = Session["authenticated"] as UserReference;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                friend = dao.Save(friend, user);
            }
            catch (Exception)
            {
                return View("Edit", friend);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid? id)
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
                    var friend = dao.GetById(id.Value, user);
                    return View(friend);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Não foi possível carregar o amigo. " + ex.Message;
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Friend friend)
        {
            user = Session["authenticated"] as UserReference;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                dao.Update(friend, user);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Não foi possível editar o amigo. " + ex.Message;
                return View("Edit", friend);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
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
                TempData["Error"] = "Não foi possível remover o amigo. " + ex.Message;
                return RedirectToAction("Index");
            }
        }

    }
}