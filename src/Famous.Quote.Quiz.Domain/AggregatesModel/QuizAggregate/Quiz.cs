using Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate;
using Famous.Quote.Quiz.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;

namespace Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate
{
    public class Quiz : Entity, IAggregateRoot
    {
        public Quiz()
        {

        }

        private Quiz(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public bool IsPublished { get; private set; }

        public virtual ICollection<Question> Questions { get; private set; }
        public virtual ICollection<UserQuiz> UserQuizzes { get; private set; }

        public static Quiz CreateNew(string name, bool isPublished)
        {
            var quiz = new Quiz(name);

            if (isPublished)
            {
                quiz.MarkAsPublished();
            }
            else
            {
                quiz.MarkAsUnPublished();
            }

            return quiz;
        }

        public void UpdateMetaData(string name)
        {
            Name = name;
        }

        public void MarkAsPublished()
        {
            IsPublished = true;
        }

        public void MarkAsUnPublished()
        {
            IsPublished = false;
        }

        public void AddNewQuestion(Question question)
        {
            if (Questions.Equals(default))
            {
                Questions = new List<Question> { question };
            }
            else
            {
                Questions.Add(question);
            }
        }

        public void DeleteQuestion(int id)
        {
            if (!Questions.Equals(default))
            {
                var question = Questions.FirstOrDefault(q => q.Id == id);

                if (question != default)
                {
                    Questions.Remove(question);
                }
            }
        }

        public Question GetQuestionById(int id)
        {
            return Questions.Equals(default) ? default : Questions.FirstOrDefault(q => q.Id == id);
        }
    }
}
