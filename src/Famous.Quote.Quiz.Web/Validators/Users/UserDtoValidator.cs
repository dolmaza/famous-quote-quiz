using Famous.Quote.Quiz.Application.Services;
using Famous.Quote.Quiz.Application.Services.DataModels;
using FluentValidation;

namespace Famous.Quote.Quiz.Web.Validators.Users
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator(IUserService userService)
        {
            RuleFor(user => user.UserName)
                .NotEmpty()
                .NotNull()
                .MustAsync(async (user, value, cancellationToken) => !(await userService.IsUserUsernameNotUnique(value, user.Id)))
                .WithMessage("User Name must be unique");

            RuleFor(user => user.FirstName)
                .NotEmpty()
                .NotNull();

            RuleFor(user => user.LastName)
                .NotEmpty()
                .NotNull();

            RuleFor(user => user.Status)
                .NotEmpty()
                .NotNull();

            RuleFor(user => user.Role)
                .NotEmpty()
                .NotNull();
        }
    }
}
