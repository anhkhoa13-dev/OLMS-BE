using Application.Queries.Courses;
using Application.Queries.Instructors;
using Domain.AccountAggregate;
using Domain.CourseAggregate;
using Domain.InstructorAggregate;
using Domain.IRepository;
using Domain.StudentAggregate;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Commands;
using Infrastructure.Database.Repositories.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IAccountRepository, AccountRepository>();

        services.AddScoped<IInstructorRepository, InstructorRepository>();
        services.AddScoped<IInstructorQuery, InstructorQuery>();

        services.AddScoped<IStudentRepository, StudentRepository>();

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseQuery, CourseQuery>();



        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
