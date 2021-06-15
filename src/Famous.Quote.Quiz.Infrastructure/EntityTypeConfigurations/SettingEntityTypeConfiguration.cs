using Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Famous.Quote.Quiz.Infrastructure.EntityTypeConfigurations
{
    public class SettingEntityTypeConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("Settings");

            builder.HasKey(entity => entity.Id);

            builder.HasOne(setting => setting.User)
                .WithOne(user => user.Setting)
                .HasForeignKey<Setting>(setting => setting.UserId)
                .IsRequired();
        }
    }
}
