using Microsoft.AspNetCore.Mvc;
using TBD.Core.Validation;
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
    }
}
