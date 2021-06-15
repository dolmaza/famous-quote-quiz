using Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Famous.Quote.Quiz.Infrastructure.EntityTypeConfigurations
{
    public class UserQuizAnswerEntityTypeConfiguration : IEntityTypeConfiguration<UserQuizAnswer>
    {
        public void Configure(EntityTypeBuilder<UserQuizAnswer> builder)
        {
            builder.ToTable("UserQuizAnswers");

            builder.HasKey(entity => entity.Id);

            builder.HasOne(userQuizAnswer => userQuizAnswer.UserQuiz)
                .WithMany(userQuiz => userQuiz.UserQuizAnswers)
                .HasForeignKey(userQuizAnswer => userQuizAnswer.UserQuizId)
                .IsRequired();

            builder.HasOne(userQuizAnswer => userQuizAnswer.Answer)
                .WithOne(answer => answer.UserQuizAnswer)
                .HasForeignKey<UserQuizAnswer>(userQuizAnswer => userQuizAnswer.AnswerId)
                .IsRequired();
        }
    }
}
