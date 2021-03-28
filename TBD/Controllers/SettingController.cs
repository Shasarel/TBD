using Microsoft.AspNetCore.Mvc;
using TBD.Core.Authorization;

namespace TBD.Controllers
{
    public class SettingController : TBDController
    {

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
