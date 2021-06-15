using Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Famous.Quote.Quiz.Infrastructure.EntityTypeConfigurations
{
    public class UserQuizEntityTypeConfiguration : IEntityTypeConfiguration<UserQuiz>
    {
        public void Configure(EntityTypeBuilder<UserQuiz> builder)
        {
            builder.ToTable("UserQuizzes");

            builder.HasKey(entity => entity.Id);

            builder.HasOne(userQuiz => userQuiz.User)
                .WithMany(user => user.UserQuizzes)
                .HasForeignKey(userQuiz => userQuiz.UserId)
                .IsRequired();

            builder.HasOne(userQuiz => userQuiz.Quiz)
                .WithMany(quiz => quiz.UserQuizzes)
                .HasForeignKey(userQuiz => userQuiz.QuizId)
                .IsRequired();
        }
    }
}
