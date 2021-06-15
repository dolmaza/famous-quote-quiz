using Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Famous.Quote.Quiz.Infrastructure.EntityTypeConfigurations
{
    public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.QuestionText)
                .IsRequired();

            builder.HasOne(question => question.Quiz)
                .WithMany(quiz => quiz.Questions)
                .HasForeignKey(question => question.QuizId)
                .IsRequired();
        }
    }
}
