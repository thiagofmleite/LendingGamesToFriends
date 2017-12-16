using LendGamesToMyFriends.DAOs;
using LendGamesToMyFriends.Models;
using System;
using System.Web.Mvc;

namespace LendGamesToMyFriends.Controllers
{
    public class HomeController : Controller
    {
        private IUsersDAO dao;

        public HomeController()
        {
            dao = new UsersDAO();
        }

        public ActionResult Index()
        {
            if (Session["authenticated"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Dashboard");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!dao.UserExists(user.Email))
                    {
                        var registeredUser = dao.Register(user);
                    }
                    else
                    {
                        ViewBag.Error = "E-mail já cadastrado";
                        return View(user);
                    }
                }
                else
                {
                    ViewBag.Error = "Verifique os campos informados";
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(user);
            }
            return RedirectToAction("NewUser", "Home");
        }

        public ActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "E-mail e senha são obrigatórios";
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    if (!dao.UserExists(email))
                    {
                        ViewBag.Error = "Usuário não registrado";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var referencedUser = dao.Authenticate(email, password);
                        Session.Add("authenticated", referencedUser);
                        return RedirectToAction("Dashboard");
                    }
                }
                catch (Exception)
                {
                    ViewBag.Error = "E-mail e senha são obrigatórios";
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Dashboard()
        {
            if (Session["authenticated"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }

}