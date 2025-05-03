
using Domain.CourseAggregate;
using Domain.IRepository;
using Domain.Results;
using MediatR;

namespace Application.Commands.Courses;

public record RemoveSectionCommand(Guid CourseId, Guid SectionId) : IRequest<Result>
{
}

public class RemoveSectionCommandHandler(
    ICourseRepository courseRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RemoveSectionCommand, Result>
{
    private readonly ICourseRepository _courseRepository = courseRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemoveSectionCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
        if (course is null)
            return new Error("Course not found");

        course.RemoveSection(request.SectionId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}