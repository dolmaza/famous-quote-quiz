using Famous.Quote.Quiz.Application.Services.DataModels;
using FluentValidation;

namespace Famous.Quote.Quiz.Web.Validators.Quizzes
{
    public class QuizDtoValidator : AbstractValidator<QuizDto>
    {
        public QuizDtoValidator()
        {
            RuleFor(quiz => quiz.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}
