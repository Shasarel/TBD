using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TBD.Controllers
{
    public class TBDController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                Request.Headers.TryGetValue("NoLayout", out var noLayout);
                ViewBag.NoLayout = bool.Parse(noLayout);
            }
            catch
            {
                ViewBag.NoLayout = false;
            }
        }
    }
}
