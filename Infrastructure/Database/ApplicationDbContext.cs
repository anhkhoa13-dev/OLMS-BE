using Domain.AccountAggregate;
using Domain.CourseAggregate;
using Domain.InstructorAggregate;
using Domain.StudentAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
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

