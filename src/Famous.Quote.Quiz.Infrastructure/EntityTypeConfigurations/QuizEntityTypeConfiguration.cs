using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Famous.Quote.Quiz.Infrastructure.EntityTypeConfigurations
{
    public class QuizEntityTypeConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.QuizAggregate.Quiz>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.QuizAggregate.Quiz> builder)
        {
            builder.ToTable("Quizzes");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Name)
                .IsRequired();
        }
    }
}
