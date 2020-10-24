using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private readonly IMapper mapper;
    private static List<Character> characters = new List<Character> {
            new Character {Id = 1},
            new Character {Id = 2, Name = "Shin"},
        };

    public CharacterService(IMapper mapper)
    {
      this.mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> Add(AddCharacterDto newCharacter)
    {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

      Character character = this.mapper.Map<Character>(newCharacter);
      character.Id = characters.Max(c => c.Id) + 1;
      characters.Add(character);
      serviceResponse.Data = (characters.Select(c => this.mapper.Map<GetCharacterDto>(c))).ToList();

      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
    {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

      serviceResponse.Data = (characters.Select(c => this.mapper.Map<GetCharacterDto>(c))).ToList();

      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetById(int id)
    {
      ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

      GetCharacterDto character = this.mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));
      serviceResponse.Data = character;

      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto updateCharacter)
    {
      ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

      try
      {
        Character character = characters.FirstOrDefault(c => c.Id == updateCharacter.Id);

        character.Name = updateCharacter.Name;
        character.Class = updateCharacter.Class;
        character.HitPoints = updateCharacter.HitPoints;
        character.Defense = updateCharacter.Defense;
        character.Intelligence = updateCharacter.Intelligence;
        character.Strength = updateCharacter.Strength;

        serviceResponse.Data = this.mapper.Map<GetCharacterDto>(character);
      }
      catch (Exception ex)
      {
        serviceResponse.Status = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id)
    {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

      try
      {
        Character character = characters.First(c => c.Id == id);
        characters.Remove(character);

        serviceResponse.Data = (characters.Select(c => this.mapper.Map<GetCharacterDto>(c))).ToList();
      }
      catch (Exception ex)
      {
        serviceResponse.Status = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }
  }
}