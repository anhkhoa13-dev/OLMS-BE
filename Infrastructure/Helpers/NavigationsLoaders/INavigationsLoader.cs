using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Helpers.NavigationsLoaders;

public interface INavigationsLoader
{
    Task LoadNavigationsAsync(Guid quizId, ApplicationDbContext context, CancellationToken cancellationToken);
}
