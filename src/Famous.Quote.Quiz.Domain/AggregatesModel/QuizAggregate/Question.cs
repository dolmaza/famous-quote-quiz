using Famous.Quote.Quiz.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;

namespace Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate
{
    public class Question : Entity, IAggregateRoot
    {
        public Question()
        {

        }

        private Question(string questionText, int sortIndex)
        {
            QuestionText = questionText;
            SortIndex = sortIndex;
        }

        public int QuizId { get; private set; }
        public string QuestionText { get; private set; }
        public bool IsActive { get; private set; }
        public int SortIndex { get; private set; }

        public virtual Quiz Quiz { get; private set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public static Question CreateNew(string questionText, bool isActive, int sortIndex)
        {
            var question = new Question(questionText, sortIndex);

            if (isActive)
            {
                question.MarkAsActive();
            }
            else
            {
                question.MarkAsNotActive();
            }

            return question;
        }

        public void MarkAsActive()
        {
            IsActive = true;
        }

        public void MarkAsNotActive()
        {
            IsActive = false;
        }

        public void UpdateMetaData(string questionText, int sortIndex)
        {
            QuestionText = questionText;
            SortIndex = sortIndex;
        }

        public void AddNewAnswer(Answer answer)
        {
            Answers ??= new List<Answer>();

            if (answer.IsCorrect)
            {
                var correctAnswer = Answers.FirstOrDefault(a => a.IsCorrect);

                correctAnswer?.MarkAsNotCorrect();
            }

            Answers.Add(answer);
        }
    }
}
