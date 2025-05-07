using Domain.Aggregates.QuizAggregate.MultipleChoiceQuestionEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.QuizAggregateConfigurations.QuestionEntities;

public sealed class ChoiceConfiguration : IEntityTypeConfiguration<Choice>
{
    public void Configure(EntityTypeBuilder<Choice> builder)
    {
        builder.ToTable("MultipleChoiceQuestion_Choices");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Text)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.IsCorrect);

        builder.Property(c => c.MultipleChoiceQuestionId)
            .IsRequired();
    }
}
