using Famous.Quote.Quiz.Application.Services.DataModels;
using Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate;
using Famous.Quote.Quiz.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Famous.Quote.Quiz.Application.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _repository;

        public QuizService(IQuizRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<QuizDto>> GetQuizzesAsync(bool? isPublished)
        {
            var quizzes = await _repository.GetQuizzesAsync(isPublished);

            return quizzes.Select(quiz => GetQuizDtoFromQuiz(quiz)).ToList();
        }

        public async Task<QuizDto> GetQuizWithQuestionsAsync(int id)
        {
            var quiz = await _repository.FindByIdAsync(id);

            if (quiz == default)
            {
                return default;
            }

            return GetQuizDtoFromQuiz(quiz, true);
        }

        public async Task<(QuizDto, QuestionDto)> GetQuizQuestionWithQuizAsync(int quizId, int id)
        {
            var quiz = await _repository.FindByIdAsync(quizId);

            var question = quiz?.GetQuestionById(id);

            if (question == null)
            {
                return default;
            }

            return (GetQuizDtoFromQuiz(quiz), GetQuestionDtoFromQuestion(question, true));
        }

        public async Task<QuizDto> GetSingleQuizByIdAsync(int id)
        {
            var quiz = await _repository.FindByIdAsync(id);

            if (quiz == default)
            {
                return default;
            }

            return GetQuizDtoFromQuiz(quiz);
        }

        public async Task<int> AddNewQuizAsync(QuizDto quizDto)
        {
            var quiz = Domain.AggregatesModel.QuizAggregate.Quiz.CreateNew(quizDto.Name, quizDto.IsPublished);

            await _repository.AddAsync(quiz);

            await _repository.SaveChangesAsync();

            return quiz.Id;
        }

        public async Task UpdateQuizAsync(QuizDto quizDto)
        {
            var quiz = await _repository.FindByIdAsync(quizDto.Id);

            if (quiz == default)
            {
                throw new FamousQuoteQuizDomainException("Quiz not found");
            }

            quiz.UpdateMetaData(quizDto.Name);

            if (quizDto.IsPublished)
            {
                quiz.MarkAsPublished();
            }
            else
            {
                quiz.MarkAsUnPublished();
            }

            _repository.Update(quiz);

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteQuizAsync(int id)
        {
            var quiz = await _repository.FindByIdAsync(id);

            if (quiz == default)
            {
                throw new FamousQuoteQuizDomainException("Quiz not found");
            }

            _repository.Remove(quiz);

            await _repository.SaveChangesAsync();
        }

        public async Task AddNewQuizQuestionAsync(int id, QuestionDto questionDto)
        {
            var quiz = await _repository.FindByIdAsync(id);

            if (quiz == default)
            {
                throw new FamousQuoteQuizDomainException("Quiz not found");
            }

            quiz.AddNewQuestion(Question.CreateNew(questionDto.QuestionText, questionDto.IsActive, questionDto.SortIndex));

            _repository.Update(quiz);

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateQuizQuestionAsync(int quizId, QuestionDto questionDto)
        {
            var quiz = await _repository.FindByIdAsync(quizId);

            var question = quiz.GetQuestionById(questionDto.Id);

            if (question == default)
            {
                throw new FamousQuoteQuizDomainException("Question not found");
            }

            question.UpdateMetaData(questionDto.QuestionText, questionDto.SortIndex);

            if (questionDto.IsActive)
            {
                question.MarkAsActive();
            }
            else
            {
                question.MarkAsNotActive();
            }

            _repository.Update(quiz);

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteQuizQuestionAsync(int quizId, int id)
        {
            var quiz = await _repository.FindByIdAsync(quizId);

            if (quiz == default)
            {
                throw new FamousQuoteQuizDomainException("Quiz not found");
            }

            quiz.DeleteQuestion(id);

            _repository.Update(quiz);

            await _repository.SaveChangesAsync();
        }

        public async Task CreateQuestionAnswerAsync(int quizId, int questionId, AnswerDto answerDto)
        {
            var quiz = await _repository.FindByIdAsync(quizId);

            var question = quiz?.GetQuestionById(questionId);

            if (question == default)
            {
                throw new FamousQuoteQuizDomainException("Question not found");
            }

            var answer = new Answer(answerDto.AnswerText, answerDto.SortIndex, answerDto.IsCorrect);
            question.AddNewAnswer(answer);

            _repository.Update(quiz);

            await _repository.SaveChangesAsync();
        }

        private QuizDto GetQuizDtoFromQuiz(Domain.AggregatesModel.QuizAggregate.Quiz quiz, bool includeQuestions = false)
        {
            return new QuizDto
            {
                Id = quiz.Id,
                Name = quiz.Name,
                IsPublished = quiz.IsPublished,
                QuestionsCount = quiz.Questions.Count,
                Questions = includeQuestions
                    ? quiz.Questions.OrderBy(ob => ob.SortIndex).Select(question => GetQuestionDtoFromQuestion(question)).ToList()
                    : new List<QuestionDto>()
            };
        }

        private QuestionDto GetQuestionDtoFromQuestion(Question question, bool includeAnswers = false)
        {
            return new QuestionDto
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                IsActive = question.IsActive,
                QuizId = question.QuizId,
                SortIndex = question.SortIndex,
                Answers = includeAnswers
                    ? question.Answers.OrderBy(ob => ob.SortIndex).Select(answer => new AnswerDto
                    {
                        Id = answer.Id,
                        AnswerText = answer.AnswerText,
                        IsCorrect = answer.IsCorrect,
                        QuestionId = answer.QuestionId,
                        SortIndex = answer.SortIndex
                    }).ToList()
                    : new List<AnswerDto>()
            };
        }
    }
}
