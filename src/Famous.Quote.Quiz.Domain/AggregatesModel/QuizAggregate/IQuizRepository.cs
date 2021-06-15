using Famous.Quote.Quiz.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate
{
    public interface IQuizRepository : IRepository<Quiz>
    {
        Task<List<Quiz>> GetQuizzesAsync(bool? isPublished);

    }
}
