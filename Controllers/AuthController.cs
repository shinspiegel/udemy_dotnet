using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using udemy_dotnet_rpg.Database;
using udemy_dotnet_rpg.Dtos.User;
using udemy_dotnet_rpg.Models;

namespace udemy_dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            this.authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUser body)
        {
            User user = new User();
            user.UserName = body.UserName;

            ServiceResponse<int> response = await this.authRepo.Register(user, body.Password);

            if (!response.Status)
            {
                return BadRequest(response);
            }

            return Created("", response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser body)
        {
            ServiceResponse<string> response = await this.authRepo.Login(body.UserName, body.Password);

            if (!response.Status)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}