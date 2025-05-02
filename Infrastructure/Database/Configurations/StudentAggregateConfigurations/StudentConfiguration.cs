using Domain.AccountAggregate;
using Domain.StudentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.StudentAggregateConfigurations;

public class InstructorConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasOne<Account>()
            .WithOne()
            .HasForeignKey<Student>(i => i.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
