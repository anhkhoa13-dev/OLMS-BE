using Domain.CourseAggregate;
using Domain.IRepository;
using Domain.Results;
using MediatR;

namespace Application.Commands.Courses;

public record AddSectionCommand(Guid CourseId, string Title, string Description) : IRequest<Result>
{
}

public class AddSectionCommandHandler(
    ICourseRepository courseRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<AddSectionCommand, Result>
{
    private readonly ICourseRepository _courseRepository = courseRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(AddSectionCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
        if (course is null)
            return new Error("Course not found");

        course.AddSection(request.Title, request.Description);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
