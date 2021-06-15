using Famous.Quote.Quiz.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public User()
        {

        }

        private User(UserRole role, string userName, string password, string firstName, string lastName)
        {
            Role = role;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public UserStatus Status { get; private set; }
        public UserRole Role { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public virtual Setting Setting { get; private set; }

        public virtual ICollection<UserQuiz> UserQuizzes { get; private set; }


        public void UpdateMetaData(UserRole role, string userName, string password, string firstName, string lastName)
        {
            Role = role;
            UserName = userName;
            Password ??= password;
            FirstName = firstName;
            LastName = lastName;
        }

        public static User CreateNew(UserStatus status, UserRole role, string userName, string password, string firstName, string lastName)
        {
            var user = new User(role, userName, password, firstName, lastName);

            switch (status)
            {
                case UserStatus.Active:
                    user.MarkAsActive();
                    break;
                case UserStatus.Disabled:
                    user.MarkAsDisabled();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, "Status is not valid!");
            }

            return user;
        }

        public void MarkAsActive()
        {
            Status = UserStatus.Active;
        }

        public void MarkAsDisabled()
        {
            Status = UserStatus.Disabled;
        }

        public void UpdateQuizMode(QuizMode quizMode)
        {
            if (Setting == default)
            {
                Setting = new Setting(quizMode);
            }
            else
            {
                Setting.UpdateQuizMode(quizMode);
            }
        }

        public void AddNewQuiz(UserQuiz userQuiz)
        {
            if (UserQuizzes.Equals(default))
            {
                UserQuizzes = new List<UserQuiz> { userQuiz };
            }
            else
            {
                UserQuizzes.Add(userQuiz);
            }
        }
    }
}
