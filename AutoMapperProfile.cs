using AutoMapper;
using udemy_dotnet_rpg.Dtos.Character;
using udemy_dotnet_rpg.Models;

namespace udemy_dotnet_rpg
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<Character, GetCharacterDto>();
      CreateMap<AddCharacterDto, Character>();
      CreateMap<UpdateCharacterDto, Character>();
    }
  }
}