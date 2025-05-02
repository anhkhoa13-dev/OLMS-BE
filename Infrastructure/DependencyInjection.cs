using Domain.AccountAggregate;
using Domain.InstructorAggregate;
using Domain.IRepository;
using Domain.StudentAggregate;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Commands;
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
        services.AddScoped<IStudentRepository, StudentRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
