namespace Famous.Quote.Quiz.Application.Services.DataModels
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        public int SortIndex { get; set; }
        public bool IsCorrect { get; set; }
    }
}