using Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate;
using Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate;
using Famous.Quote.Quiz.Domain.Extensions;
using Famous.Quote.Quiz.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Famous.Quote.Quiz.Infrastructure
{
    public class FamousQuoteQuizDbContext : DbContext
    {
        public FamousQuoteQuizDbContext(DbContextOptions<FamousQuoteQuizDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityTypeConfiguration).Assembly);
        }
    }

    public class DbInitializer
    {
        public static async Task Initialize(FamousQuoteQuizDbContext context)
        {
            if (!await context.Set<User>().AnyAsync())
            {
                var adminUser = User.CreateNew(UserStatus.Active, UserRole.Admin, "admin", "admin".ToSha256(), "admin", "admin");
                adminUser.DateOfCreate = DateTimeOffset.UtcNow;

                var user = User.CreateNew(UserStatus.Active, UserRole.User, "user", "user".ToSha256(), "user", "user");
                user.DateOfCreate = DateTimeOffset.UtcNow;

                await context.AddAsync(adminUser);
                await context.AddAsync(user);

                await context.SaveChangesAsync();

                var quiz = Domain.AggregatesModel.QuizAggregate.Quiz.CreateNew("Quiz #1", true);
                quiz.DateOfCreate = DateTimeOffset.UtcNow;

                var question1 = Question.CreateNew
                (
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sapien lacus, feugiat posuere dignissim et, varius at orci. Curabitur at facilisis eros. Aenean imperdiet sem eu orci dapibus varius",
                    true,
                    0
                );

                question1.DateOfCreate = DateTimeOffset.UtcNow;

                question1.AddNewAnswer(new Answer("Answer #1", 0, true));
                question1.AddNewAnswer(new Answer("Answer #2", 1, false));
                question1.AddNewAnswer(new Answer("Answer #3", 2, false));
                question1.AddNewAnswer(new Answer("Answer #4", 3, false));

                quiz.AddNewQuestion(question1);

                var question2 = Question.CreateNew
                (
                    "Etiam eu elit eget metus elementum molestie vel at urna. Proin sollicitudin, felis a consequat viverra, tellus leo euismod tellus, non dapibus purus diam eu dolor",
                    true,
                    1
                );

                question2.DateOfCreate = DateTimeOffset.UtcNow;

                question2.AddNewAnswer(new Answer("Answer #1", 0, false));
                question2.AddNewAnswer(new Answer("Answer #2", 1, false));
                question2.AddNewAnswer(new Answer("Answer #3", 2, true));
                question2.AddNewAnswer(new Answer("Answer #4", 3, false));

                quiz.AddNewQuestion(question2);

                var question3 = Question.CreateNew
                (
                    "Quisque nec arcu et lectus tempor vulputate. Curabitur quis sem ut tortor semper fringilla. Fusce vel pretium erat. Nam bibendum, metus eu hendrerit laoreet",
                    true,
                    2
                );

                question3.DateOfCreate = DateTimeOffset.UtcNow;

                question3.AddNewAnswer(new Answer("Answer #1", 0, false));
                question3.AddNewAnswer(new Answer("Answer #2", 1, false));
                question3.AddNewAnswer(new Answer("Answer #3", 2, false));
                question3.AddNewAnswer(new Answer("Answer #4", 3, true));

                quiz.AddNewQuestion(question3);

                var question4 = Question.CreateNew
                (
                    "lacus lectus sodales tortor, lobortis vehicula arcu turpis sed dolor. Morbi in mi non libero iaculis dictum sit amet id ex. Pellentesque habitant morbi tristique",
                    true,
                    3
                );

                question4.DateOfCreate = DateTimeOffset.UtcNow;

                question4.AddNewAnswer(new Answer("Answer #1", 0, false));
                question4.AddNewAnswer(new Answer("Answer #2", 1, false));
                question4.AddNewAnswer(new Answer("Answer #3", 2, true));
                question4.AddNewAnswer(new Answer("Answer #4", 3, false));

                quiz.AddNewQuestion(question4);

                var question5 = Question.CreateNew
                (
                    "Mauris porttitor lectus nibh, quis porttitor mi consectetur ut. Sed aliquam venenatis varius. Aenean fermentum diam feugiat, euismod velit nec, rutrum mi",
                    true,
                    4
                );

                question5.DateOfCreate = DateTimeOffset.UtcNow;

                question5.AddNewAnswer(new Answer("Answer #1", 0, false));
                question5.AddNewAnswer(new Answer("Answer #2", 1, true));
                question5.AddNewAnswer(new Answer("Answer #3", 2, false));
                question5.AddNewAnswer(new Answer("Answer #4", 3, false));

                quiz.AddNewQuestion(question5);

                await context.AddAsync(quiz);

                await context.SaveChangesAsync();
            }
        }
    }
}
