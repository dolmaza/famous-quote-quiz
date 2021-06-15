using Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate;
using Famous.Quote.Quiz.Domain.SeedWork;

namespace Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate
{
    public class UserQuizAnswer : Entity
    {
        public UserQuizAnswer(int answerId)
        {
            AnswerId = answerId;
        }

        public int UserQuizId { get; private set; }
        public int AnswerId { get; private set; }

        public virtual UserQuiz UserQuiz { get; private set; }
        public virtual Answer Answer { get; private set; }
    }
}