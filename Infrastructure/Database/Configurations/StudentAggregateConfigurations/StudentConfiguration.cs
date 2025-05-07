using Domain.Aggregates.AccountAggregate;
using Domain.Aggregates.StudentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.StudentAggregateConfigurations;

public sealed class InstructorConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Student");
        builder.HasKey(i => i.Id);

        builder.HasOne<Account>()
            .WithOne()
            .HasForeignKey<Student>(i => i.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
