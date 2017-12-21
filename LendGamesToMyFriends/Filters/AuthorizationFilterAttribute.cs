using System.Web.Mvc;

namespace LendGamesToMyFriends.Filters
{
    public class AuthorizationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object authenticated = filterContext.HttpContext.Session["authenticated"];
            if (authenticated == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "Home", action = "Index" }
                    )
                );
            }
        }
    }
}