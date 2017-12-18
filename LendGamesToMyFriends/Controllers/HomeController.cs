using LendGamesToMyFriends.DAOs;
using LendGamesToMyFriends.Models;
using System;
using System.Web.Mvc;

namespace LendGamesToMyFriends.Controllers
{
    public class HomeController : Controller
    {
        private IUsersDAO dao;
        private IGamesDAO gamesDao;

        public HomeController()
        {
            dao = new UsersDAO();
            gamesDao = new GamesDAO();
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
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "E-mail e senha são obrigatórios";
                return View("Index");
            }
            else
            {
                try
                {
                    if (!dao.UserExists(login.Email))
                    {
                        ViewBag.Error = "Usuário não registrado";
                        return View("Index");
                    }
                    else
                    {
                        var referencedUser = dao.Authenticate(login);
                        Session.Add("authenticated", referencedUser);
                        return RedirectToAction("Dashboard");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Não foi possível entrar no sistem: " + ex.Message;
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Dashboard()
        {
            var user = Session["authenticated"] as UserReference;
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            else
            {

                var games = gamesDao.GetAll(user);
                return View(games);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }

}