using Domain.Aggregates.CourseAggregate;
using Domain.Aggregates.InstructorAggregate;
using Domain.IRepository;
using Domain.Results;
using MediatR;

namespace Application.Commands.Instructors;

public record CreateCourseCommand(Guid InstructorId, string Title, string Description) : IRequest<Result>
{
}

public class CreateCourseCommandHandler(
    IInstructorRepository instructorRepository,
    ICourseRepository courseRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateCourseCommand, Result>
{
    private readonly IInstructorRepository _instructorRepository = instructorRepository;
    private readonly ICourseRepository _courseRepository = courseRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId, cancellationToken);
        if (instructor is null)
        {
            return new Error("Unauthorize");
        }

        var course = instructor.CreateCourse(request.Title, request.Description);

        _instructorRepository.Update(instructor);
        await _courseRepository.AddAsync(course, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}