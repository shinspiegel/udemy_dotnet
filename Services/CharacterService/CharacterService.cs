using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using udemy_dotnet_rpg.Database;
using udemy_dotnet_rpg.Dtos.Character;
using udemy_dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace udemy_dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> Add(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = this.mapper.Map<Character>(newCharacter);

            await this.context.Characters.AddAsync(character);
            await this.context.SaveChangesAsync();

            List<Character> dbCharacter = await this.context.Characters.ToListAsync();
            serviceResponse.Data = (dbCharacter.Select(c => this.mapper.Map<GetCharacterDto>(c))).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll(int userId)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            List<Character> dbCharacter = await this.context.Characters.Where(c => c.User.Id == userId).ToListAsync();
            serviceResponse.Data = (dbCharacter.Select(c => this.mapper.Map<GetCharacterDto>(c))).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

            Character dbCharacter = await this.context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            GetCharacterDto character = this.mapper.Map<GetCharacterDto>(dbCharacter);
            serviceResponse.Data = character;

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                Character character = await this.context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);

                character.Name = updateCharacter.Name;
                character.Class = updateCharacter.Class;
                character.HitPoints = updateCharacter.HitPoints;
                character.Defense = updateCharacter.Defense;
                character.Intelligence = updateCharacter.Intelligence;
                character.Strength = updateCharacter.Strength;

                this.context.Characters.Update(character);
                await this.context.SaveChangesAsync();

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
                Character character = await this.context.Characters.FirstAsync(c => c.Id == id);
                this.context.Characters.Remove(character);
                await this.context.SaveChangesAsync();

                serviceResponse.Data = (this.context.Characters.Select(c => this.mapper.Map<GetCharacterDto>(c))).ToList();
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