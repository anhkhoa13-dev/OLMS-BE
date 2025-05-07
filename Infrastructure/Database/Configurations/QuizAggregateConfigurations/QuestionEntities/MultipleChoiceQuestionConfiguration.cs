using Domain.Aggregates.QuizAggregate;
using Domain.Aggregates.QuizAggregate.MultipleChoiceQuestionEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.QuizAggregateConfigurations.QuestionEntities;

public sealed class MultipleChoiceQuestionConfiguration : IEntityTypeConfiguration<MultipleChoiceQuestion>
{
    public void Configure(EntityTypeBuilder<MultipleChoiceQuestion> builder)
    {
        builder.ToTable("MultipleChoiceQuestion");

        builder.HasOne<Question>()
            .WithOne()
            .HasForeignKey<MultipleChoiceQuestion>(q => q.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(mcq => mcq.Choices)
            .WithOne()
            .HasForeignKey(c => c.MultipleChoiceQuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
