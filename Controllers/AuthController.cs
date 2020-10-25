using System.Threading.Tasks;
using dotnet_rpg.Database;
using dotnet_rpg.Dtos.User;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository authRepository;

    public AuthController(IAuthRepository authRepository)
    {
      this.authRepository = authRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto request)
    {
      User user = new User { UserName = request.username };

      ServiceResponse<int> response = await authRepository.Register(user, request.password);

      if (!response.Status)
      {
        return BadRequest(response);
      }

      return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto request)
    {
      ServiceResponse<string> response = await authRepository.Login(request.username, request.password);

      if (!response.Status)
      {
        return BadRequest(response);
      }

      return Ok(response);
    }
  }
}