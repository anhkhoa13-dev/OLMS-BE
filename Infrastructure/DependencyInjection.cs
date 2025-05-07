using Application.Queries.Courses;
using Application.Queries.Instructors;
using Application.Queries.Quizzes;
using Domain.Aggregates.AccountAggregate;
using Domain.Aggregates.CourseAggregate;
using Domain.Aggregates.InstructorAggregate;
using Domain.Aggregates.QuizAggregate;
using Domain.Aggregates.QuizAggregate.MultipleChoiceQuestionEntity;
using Domain.Aggregates.QuizAggregate.ShortAnswerQuestionEntity;
using Domain.Aggregates.StudentAggregate;
using Domain.IRepository;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Commands;
using Infrastructure.Database.Repositories.Queries;
using Infrastructure.Helpers.NavigationsLoaders;
using Infrastructure.Helpers.NavigationsLoaders.QuestionLoaders;
using Infrastructure.Helpers.ResponseMapperApdapters;
using Infrastructure.Helpers.ResponseMappers.QuestionResponseMappers;
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
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());

        // Repositories
        services.AddScoped<IAccountRepository, AccountRepository>();

        services.AddScoped<IInstructorRepository, InstructorRepository>();
        services.AddScoped<IInstructorQuery, InstructorQuery>();

        services.AddScoped<IStudentRepository, StudentRepository>();

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseQuery, CourseQuery>();

        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IQuizQuery, QuizQuery>();


        // Mappers & Adapters
        services.AddScoped<IQuestionDtoMapper, QuestionResponseMapperAdapter<MultipleChoiceQuestion>>();
        services.AddScoped<IQuestionResponseMapper<MultipleChoiceQuestion>, MultipleChoiceQuestionResponseMapper>();

        services.AddScoped<IQuestionDtoMapper, QuestionResponseMapperAdapter<ShortAnswerQuestion>>();
        services.AddScoped<IQuestionResponseMapper<ShortAnswerQuestion>, ShortAnswerQuestionResponseMapper>();

        // Navigation Loaders
        services.AddScoped<INavigationsLoader, MultipleChoiceQuestionLoader>();
        services.AddScoped<INavigationsLoader, ShortAnswerQuestionLoader>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
