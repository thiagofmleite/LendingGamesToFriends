using LendGamesToMyFriends.DAOs;
using LendGamesToMyFriends.Filters;
using LendGamesToMyFriends.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LendGamesToMyFriends.Controllers
{
    [AuthorizationFilter]
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
            user = Session["authenticated"] as UserReference;
            var lendings = dao.GetAll(user);
            return View(lendings);
        }

        [HttpGet]
        public ActionResult New(Guid? gameId)
        {
            user = Session["authenticated"] as UserReference;
            ViewBag.Friends = friendsDao.GetAll(user)
                .OrderBy(f => f.Name)
                .Select(f => new SelectListItem { Text = $"{f.Name} {f.LastName}", Value = f.Id.ToString() })
                .ToList();
            ViewBag.Games = gamesDao
                .GetAll(user)
                .OrderBy(g => g.Title)
                .Select(g => new SelectListItem { Text = g.Title, Value = g.Id.ToString(), Disabled = !g.Status, Selected = (g.Id.Equals(gameId)) })
                .ToList();
            return View(new Lending());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(Guid friendId, Guid gameId)
        {
            user = Session["authenticated"] as UserReference;
            if (ModelState.IsValid)
            {
                try
                {
                    Game game = gamesDao.GetById(gameId, user);
                    if (game.Status)
                    {
                        Lending lending = new Lending
                        {
                            FriendId = friendId,
                            GameId = gameId
                        };
                        dao.Save(lending, user);
                        game.Status = false;
                        gamesDao.Update(game, user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        throw new Exception("O jogo não está disponível para emprestar");
                    }
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

        [HttpGet]
        public ActionResult Return(Guid id)
        {
            try
            {
                user = Session["authenticated"] as UserReference;
                if (user == null)
                {
                    throw new Exception("Sessão expirada");
                }
                var lending = dao.GetById(id, user);
                if (!lending.Status)
                {
                    dao.Update(lending, user);
                    Game game = gamesDao.GetById(lending.Game.Id, user);
                    game.Status = true;
                    gamesDao.Update(game, user);
                    return Json("Jogo devolvido", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    throw new Exception("Jogo já devolvido");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}