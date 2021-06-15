using System.Collections.Generic;

namespace Famous.Quote.Quiz.Application.Services.DataModels
{
    public class QuizDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublished { get; set; }
        public int QuestionsCount { get; set; }

        public List<QuestionDto> Questions { get; set; }
    }
}
