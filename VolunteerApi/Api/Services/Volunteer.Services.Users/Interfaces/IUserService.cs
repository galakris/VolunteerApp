using System.Threading.Tasks;
using Volunteer.Services.Users.Models;

namespace Volunteer.Services.Users.Interfaces
{
    public interface IUserService
    {
        Task<UserAccountDto> Authenticate(string username, string password);

        Task<UserAccountDto> Create(RegisterRequestDto model);

        UserAccountDto GetUserById(int id);
    }
}
