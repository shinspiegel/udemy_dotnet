using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAll();
        Task<ServiceResponse<GetCharacterDto>> GetById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> Add(AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto updateCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id);
    }
}