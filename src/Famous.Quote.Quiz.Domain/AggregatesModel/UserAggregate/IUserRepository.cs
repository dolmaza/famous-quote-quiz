using Famous.Quote.Quiz.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetUsersAsync();

        Task<bool> IsUserUsernameNotUnique(string username, int? id = null);

        Task<User> GetAuthorizedUser(string username, string password);

    }
}
