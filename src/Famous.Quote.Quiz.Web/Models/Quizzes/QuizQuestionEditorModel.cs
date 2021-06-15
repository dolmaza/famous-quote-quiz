using Famous.Quote.Quiz.Application.Services.DataModels;

namespace Famous.Quote.Quiz.Web.Models.Quizzes
{
    public class QuizQuestionEditorModel
    {
        public QuizDto Quiz { get; set; }
        public QuestionDto Question { get; set; }
    }
}
