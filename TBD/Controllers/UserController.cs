using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TBD.Core.Authorization;
using TBD.Core.Validation;
using TBD.Helpers;
using TBD.Interfaces;
using TBD.Models;

namespace TBD.Controllers
{
    [Route("{Action}")]
    public class UserController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly AppSettings _appSettings;
        public UserController(IAuthorizationService authorizationService, IOptions<AppSettings> appSettings)
        {
            _authorizationService = authorizationService;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Register([FromBody] UserViewModel user)
        {
            try
            {
                _authorizationService.CreateUser(user);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelErrors(ex);
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Login([FromForm] CredentialsViewModel credentials)
        {
            var token = _authorizationService.CreateToken(credentials);

            if (token == null)
            {
                ModelState.AddModelError("Error","Niepoprawny login lub hasło");
                return View();
            }

            HttpContext.Response.Cookies.Append("Authorization", $"Bearer {token}", new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(_appSettings.TokenLifetime)
            });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
