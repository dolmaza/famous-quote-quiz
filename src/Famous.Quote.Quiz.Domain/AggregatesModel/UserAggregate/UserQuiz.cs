using Famous.Quote.Quiz.Domain.SeedWork;
using System.Collections.Generic;

namespace Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate
{
    public class UserQuiz : Entity, IAggregateRoot
    {
        public UserQuiz(int quizId)
        {
            QuizId = quizId;
        }

        public int UserId { get; private set; }
        public int QuizId { get; private set; }

        public virtual User User { get; private set; }
        public virtual QuizAggregate.Quiz Quiz { get; private set; }
        public virtual ICollection<UserQuizAnswer> UserQuizAnswers { get; private set; }

        public void AddUserQuizAnswer(UserQuizAnswer userQuizAnswer)
        {
            if (UserQuizAnswers.Equals(default))
            {
                UserQuizAnswers = new List<UserQuizAnswer> { userQuizAnswer };
            }
            else
            {
                UserQuizAnswers.Add(userQuizAnswer);
            }
        }
    }
}
