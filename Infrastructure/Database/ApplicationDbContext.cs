using Domain.Aggregates.AccountAggregate;
using Domain.Aggregates.CourseAggregate;
using Domain.Aggregates.InstructorAggregate;
using Domain.Aggregates.QuizAggregate;
using Domain.Aggregates.QuizAggregate.MultipleChoiceQuestionEntity;
using Domain.Aggregates.QuizAggregate.ShortAnswerQuestionEntity;
using Domain.Aggregates.SectionAggregate;
using Domain.Aggregates.StudentAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    //Account Aggregate
    public DbSet<Account> Accounts { get; set; }

    //Course Aggregate
    public DbSet<Course> Courses { get; set; }
    public DbSet<Section> Sections { get; set; }

    //Quiz Aggregate
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
    public DbSet<ShortAnswerQuestion> ShortAnswerQuestions { get; set; }

    //Instructor Aggregate
    public DbSet<Instructor> Instructors { get; set; }

    //Student Aggregate
    public DbSet<Student> Students { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new InvalidOperationException("Database configuration is missing.");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    }
}

