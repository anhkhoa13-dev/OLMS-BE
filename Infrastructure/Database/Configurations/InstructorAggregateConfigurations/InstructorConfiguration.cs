using Domain.AccountAggregate;
using Domain.InstructorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.InstructorAggregateConfigurations;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasOne<Account>()
            .WithOne()
            .HasForeignKey<Instructor>(i => i.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
