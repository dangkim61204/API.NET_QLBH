
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Clothing_storeAPI.Controllers
{
    public class BaseController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            String? user = User.Identity?.Name;
            String? role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            if (role == "Admin")
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    { "controller", "Login" },
                    { "action", "AccessDenied" } });
            }
        }
    }
}
