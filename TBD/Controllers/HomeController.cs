﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TBD.Core.Authorization;

namespace TBD.Controllers
{
    public class HomeController : TBDController
    {

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
