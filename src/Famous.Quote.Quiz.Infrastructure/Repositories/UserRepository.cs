using Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Famous.Quote.Quiz.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(FamousQuoteQuizDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await Query().OrderBy(ob => ob.DateOfCreate).ToListAsync();
        }

        public async Task<bool> IsUserUsernameNotUnique(string username, int? id = null)
        {
            return await Query().AnyAsync(u => (id == null && u.UserName == username) || (u.Id != id && u.UserName == username));
        }

        public async Task<User> GetAuthorizedUser(string username, string password)
        {
            return await Query().FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
    }
}
