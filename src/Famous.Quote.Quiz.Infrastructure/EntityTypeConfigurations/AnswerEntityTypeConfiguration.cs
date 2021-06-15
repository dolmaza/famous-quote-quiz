using Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Famous.Quote.Quiz.Infrastructure.EntityTypeConfigurations
{
    public class AnswerEntityTypeConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.AnswerText)
                .IsRequired();

            builder.HasOne(answer => answer.Question)
                .WithMany(question => question.Answers)
                .HasForeignKey(question => question.QuestionId)
                .IsRequired();
        }
    }
}
