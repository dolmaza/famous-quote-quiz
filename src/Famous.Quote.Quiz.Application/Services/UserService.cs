using Famous.Quote.Quiz.Application.Services.DataModels;
using Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate;
using Famous.Quote.Quiz.Domain.Exceptions;
using Famous.Quote.Quiz.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Famous.Quote.Quiz.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsUserUsernameNotUnique(string username, int? id = null)
        {
            return await _repository.IsUserUsernameNotUnique(username, id);
        }

        public async Task<UserDto> GetAuthorizedUserAsync(string username, string password)
        {
            var user = await _repository.GetAuthorizedUser(username, password.ToSha256());

            if (user == default)
            {
                return default;
            }

            return GetUserToUserDto(user);
        }

        public async Task<int> AddNewUserAsync(UserDto userDto)
        {
            var user = User.CreateNew(userDto.Status, userDto.Role, userDto.UserName, userDto.Password.ToSha256(), userDto.FirstName, userDto.LastName);

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            return user.Id;
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var user = await _repository.FindByIdAsync(userDto.Id ?? 0);

            if (user == default)
            {
                throw new FamousQuoteQuizDomainException("User not found");
            }

            user.UpdateMetaData(userDto.Role, userDto.UserName, userDto.Password.ToSha256(), userDto.FirstName, userDto.LastName);

            switch (userDto.Status)
            {
                case UserStatus.Active:
                    user.MarkAsActive();
                    break;
                case UserStatus.Disabled:
                    user.MarkAsDisabled();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _repository.Update(user);

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _repository.FindByIdAsync(id);

            if (user == default)
            {
                throw new FamousQuoteQuizDomainException("User not found");
            }

            _repository.Remove(user);

            await _repository.SaveChangesAsync();
        }

        public async Task DisableUserAsync(int id)
        {
            var user = await _repository.FindByIdAsync(id);

            if (user == default)
            {
                throw new FamousQuoteQuizDomainException("User not found");
            }

            user.MarkAsDisabled();

            _repository.Update(user);

            await _repository.SaveChangesAsync();
        }

        public async Task ActivateUserAsync(int id)
        {
            var user = await _repository.FindByIdAsync(id);

            if (user == default)
            {
                throw new FamousQuoteQuizDomainException("User not found");
            }

            user.MarkAsActive();

            _repository.Update(user);

            await _repository.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _repository.GetUsersAsync();

            return users.Select(GetUserToUserDto).ToList();
        }

        public async Task<UserDto> GetSingleUserByIdAsync(int id)
        {
            var user = await _repository.FindByIdAsync(id);

            if (user == default)
            {
                return default;
            }

            return GetUserToUserDto(user);
        }

        private UserDto GetUserToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Status = user.Status,
                UserName = user.UserName
            };
        }
    }
}