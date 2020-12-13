using System.Threading.Tasks;
using udemy_dotnet_rpg.Models;

namespace udemy_dotnet_rpg.Database
{
    public interface IAuthRepository
    {
         Task<ServiceResponse<int>> Register (User user, string password);
         Task<ServiceResponse<string>> Login (string username, string password);
         Task<bool> UserExists (string username);
    }
}