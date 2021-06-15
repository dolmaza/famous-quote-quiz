using Famous.Quote.Quiz.Domain.SeedWork;

namespace Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate
{
    public class Setting : Entity
    {
        public Setting()
        {

        }

        public Setting(QuizMode quizMode)
        {
            QuizMode = quizMode;
        }

        public int UserId { get; private set; }
        public QuizMode QuizMode { get; private set; }

        public virtual User User { get; private set; }

        public void UpdateQuizMode(QuizMode quizMode)
        {
            QuizMode = quizMode;
        }
    }
}
