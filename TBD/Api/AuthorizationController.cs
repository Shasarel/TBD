using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using TBD.DbModels;
using TBD.Interfaces;
using TBD.Models;

namespace TBD.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [HttpPost]
        public IActionResult Register([FromBody] UserViewModel user)
        {
            if (!ModelState.IsValid) return BadRequest();
            if(!_authorizationService.CreateUser(user.Login, user.Password, user.Name, user.Role)) return BadRequest();
            return Ok();

        }
    }
}
