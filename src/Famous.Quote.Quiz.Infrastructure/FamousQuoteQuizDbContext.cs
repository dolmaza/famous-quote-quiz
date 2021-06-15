using Famous.Quote.Quiz.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

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
}
