using Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Famous.Quote.Quiz.Infrastructure.Repositories
{
    public class QuizRepository : Repository<Domain.AggregatesModel.QuizAggregate.Quiz>, IQuizRepository
    {
        public QuizRepository(FamousQuoteQuizDbContext context) : base(context)
        {
        }

        public async Task<List<Domain.AggregatesModel.QuizAggregate.Quiz>> GetQuizzesAsync(bool? isPublished)
        {
            return await Query()
                .Where(q => !isPublished.HasValue || q.IsPublished == isPublished)
                .OrderBy(ob => ob.DateOfCreate)
                .ToListAsync();
        }
    }
}
