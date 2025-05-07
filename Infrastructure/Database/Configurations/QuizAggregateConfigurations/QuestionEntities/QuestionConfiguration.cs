using Domain.Aggregates.QuizAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.QuizAggregateConfigurations.QuestionEntities;

public sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Question");
        builder.HasKey(q => q.Id);

        builder.Property(q => q.Id)
            .ValueGeneratedNever();

        builder.Property(q => q.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(q => q.Order)
            .IsRequired();

        builder.Property(q => q.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(q => q.QuizId)
            .IsRequired();
    }
}

