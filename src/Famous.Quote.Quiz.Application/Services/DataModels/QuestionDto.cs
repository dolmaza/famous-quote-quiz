using System.Collections.Generic;

namespace Famous.Quote.Quiz.Application.Services.DataModels
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string QuestionText { get; set; }
        public bool IsActive { get; set; }
        public int SortIndex { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
