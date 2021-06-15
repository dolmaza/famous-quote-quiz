using Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate;
using Famous.Quote.Quiz.Domain.SeedWork;

namespace Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate
{
    public class Answer : Entity
    {
        public Answer(string answerText, int sortIndex, bool isCorrect)
        {
            AnswerText = answerText;
            SortIndex = sortIndex;
            IsCorrect = isCorrect;
        }

        public int QuestionId { get; private set; }
        public string AnswerText { get; private set; }
        public int SortIndex { get; private set; }
        public bool IsCorrect { get; private set; }

        public virtual Question Question { get; private set; }
        public virtual UserQuizAnswer UserQuizAnswer { get; private set; }

        public void MarkAsNotCorrect()
        {
            IsCorrect = false;
        }

    }
}
