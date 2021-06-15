using Famous.Quote.Quiz.Application.Services.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Famous.Quote.Quiz.Application.Services
{
    public interface IQuizService
    {
        Task<List<QuizDto>> GetQuizzesAsync(bool? isPublished);

        Task<QuizDto> GetQuizWithQuestionsAsync(int id);

        Task<QuizDto> GetSingleQuizByIdAsync(int id);

        Task<(QuizDto, QuestionDto)> GetQuizQuestionWithQuizAsync(int quizId, int id);

        Task<int> AddNewQuizAsync(QuizDto quizDto);

        Task UpdateQuizAsync(QuizDto quizDto);

        Task DeleteQuizAsync(int id);

        Task AddNewQuizQuestionAsync(int id, QuestionDto questionDto);

        Task UpdateQuizQuestionAsync(int quizId, QuestionDto questionDto);

        Task DeleteQuizQuestionAsync(int quizId, int id);

        Task CreateQuestionAnswerAsync(int quizId, int questionId, AnswerDto answerDto);
    }
}
