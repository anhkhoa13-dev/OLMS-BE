using Domain.Aggregates.QuizAggregate;
using Domain.Aggregates.QuizAggregate.ShortAnswerQuestionEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.QuizAggregateConfigurations.QuestionEntities;

public sealed class ShortAnswerQuestionConfiguration : IEntityTypeConfiguration<ShortAnswerQuestion>
{
    public void Configure(EntityTypeBuilder<ShortAnswerQuestion> builder)
    {
        builder.ToTable("ShortAnswerQuestion");

        builder.HasOne<Question>()
            .WithOne()
            .HasForeignKey<ShortAnswerQuestion>(q => q.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(q => q.CorrectAnswers, ca =>
        {
            ca.WithOwner().HasForeignKey("ShortAnswerQuestionId");
            ca.ToTable("ShortAnswerQuestion_CorrectAnswer");

            ca.Property(c => c.Text)
                .HasColumnName("Text")
                .HasMaxLength(100)
                .IsRequired();

            ca.HasKey("ShortAnswerQuestionId", "Text");
        });
        
        var navigation = builder.Metadata.FindNavigation(nameof(ShortAnswerQuestion.CorrectAnswers));
        navigation!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}


