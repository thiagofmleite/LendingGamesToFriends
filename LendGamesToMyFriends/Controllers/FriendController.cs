using LendGamesToMyFriends.DAOs;
using LendGamesToMyFriends.Models;
using System;
using System.Web.Mvc;
using LendGamesToMyFriends.Filters;

namespace LendGamesToMyFriends.Controllers
{
    [AuthorizationFilter]
    public class FriendController : Controller
    {
        private IFriendsDAO dao;
        private UserReference user;

        public FriendController()
        {
            dao = new FriendsDAO();
        }

        // GET: Friend
        public ActionResult Index(string name)
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"].ToString();
                TempData.Remove("Error");
            }
            try
            {
                user = Session["authenticated"] as UserReference;
                var friends = (string.IsNullOrEmpty(name)) ? dao.GetAll(user) : dao.GetByName(name, user);
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
            return View("Edit", new Friend());
        }

        [HttpPost]
        public ActionResult New(Friend friend)
        {
            user = Session["authenticated"] as UserReference;
            try
            {
                friend = dao.Save(friend, user);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Edit", friend);
            }
        }

        public ActionResult Edit(Guid? id)
        {
            user = Session["authenticated"] as UserReference;
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