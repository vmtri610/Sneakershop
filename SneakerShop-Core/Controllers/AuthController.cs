using Microsoft.AspNetCore.Mvc;

namespace SneakerShop_Core.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AuthController : Controller
    {
        private AuthService.Auth.AuthClient _authClient;

        public AuthController(AuthService.Auth.AuthClient authClient)
        {
            this._authClient = authClient;
        }

        [HttpPost]
        public async Task<AuthService.LoginReply> Create(AuthService.LoginRequest loginRequest)
        {
            var result = await _authClient.LoginAsync(loginRequest);
            return result;
        }

    }
}
