using Domain.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.AccountAggregateConfigurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Password)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Role)
            .IsRequired()
            .HasConversion<string>();


    }
}