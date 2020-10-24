using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CharacterController : ControllerBase
  {
    private readonly ICharacterService characterService;

    public CharacterController(ICharacterService characterService)
    {
      this.characterService = characterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      ServiceResponse<List<GetCharacterDto>> response = await this.characterService.GetAll();
      return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id)
    {
      ServiceResponse<GetCharacterDto> response = await this.characterService.GetById(id);
      return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
    {
      ServiceResponse<List<GetCharacterDto>> response = await this.characterService.Add(newCharacter);
      return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updateCharacter)
    {
      ServiceResponse<GetCharacterDto> response = await this.characterService.Update(updateCharacter);

      if (response.Data == null)
      {
        return NotFound(response);
      }

      return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacter(int id)
    {
      ServiceResponse<List<GetCharacterDto>> response = await this.characterService.Delete(id);

      if (response.Data == null)
      {
        return NotFound(response);
      }

      return Ok(response);
    }
  }
}