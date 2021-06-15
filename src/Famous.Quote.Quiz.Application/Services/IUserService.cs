using Famous.Quote.Quiz.Application.Services.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Famous.Quote.Quiz.Application.Services
{
    public interface IUserService
    {
        Task<bool> IsUserUsernameNotUnique(string username, int? id = null);

        Task<UserDto> GetAuthorizedUserAsync(string username, string password);

        Task<int> AddNewUserAsync(UserDto userDto);

        Task UpdateUserAsync(UserDto userDto);

        Task<List<UserDto>> GetUsersAsync();

        Task<UserDto> GetSingleUserByIdAsync(int id);

        Task DeleteUserAsync(int id);

        Task DisableUserAsync(int id);

        Task ActivateUserAsync(int id);
    }
}
