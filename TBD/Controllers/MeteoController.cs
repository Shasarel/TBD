using Microsoft.AspNetCore.Mvc;
using TBD.Core.Authorization;

namespace TBD.Controllers
{
    public class MeteoController : TBDController
    {

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
