using Domain.CourseAggregate;
using Domain.InstructorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.CourseAggregateConfigurations;

public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.OwnsOne(c => c.Code, c=>
        {
            c.Property(c => c.Value)
                .IsRequired()
                .HasMaxLength(6);

            c.HasIndex(c => c.Value).IsUnique();
        });

        builder.HasOne<Instructor>()
            .WithMany()
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
