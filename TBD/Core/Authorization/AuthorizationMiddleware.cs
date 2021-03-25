using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using TBD.Interfaces;

namespace TBD.Core.Authorization
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAuthorizationService authorizationService)
        {
            var token = context.Request.Cookies["Authorization"].Split(" ").Last();
            context.Items["User"] = authorizationService.ValidateToken(token);
            await _next(context);
        }
    }
}