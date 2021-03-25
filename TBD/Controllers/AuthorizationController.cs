using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TBD.Core.Validation;
using TBD.Helpers;
using TBD.Interfaces;
using TBD.Models;

namespace TBD.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly AppSettings _appSettings;
        public AuthorizationController(IAuthorizationService authorizationService, IOptions<AppSettings> appSettings)
        {
            _authorizationService = authorizationService;
            _appSettings = appSettings.Value;
        }
        [HttpPost]
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

        [HttpGet]
        public IActionResult Login([FromBody] CredentialsViewModel credentials)
        {
            var token = _authorizationService.CreateToken(credentials);

            if (token == null) return Unauthorized("Nieprawidłowy login lub hasło");

            HttpContext.Response.Cookies.Append("Authorization", $"Bearer {token}", new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(_appSettings.TokenLifetime)
            });

            return Ok(token);
        }
    }
}
