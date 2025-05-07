using Domain.Aggregates.AccountAggregate;
using Domain.Aggregates.InstructorAggregate;
using Domain.Aggregates.StudentAggregate;
using Domain.IRepository;
using Domain.Results;
using MediatR;

namespace Application.Commands.Accounts;

public record CreateAccountCommand(string Username, string Password, string Fullname, Role Role) : IRequest<Result>
{
}

public class CreateAccountCommandHandler(
    IAccountRepository accountRepository,
    IStudentRepository studentRepository,
    IInstructorRepository instructorRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateAccountCommand, Result>
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IStudentRepository _studentRepository = studentRepository;
    private readonly IInstructorRepository _instructorRepository = instructorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        if (await _accountRepository.IsUsernameUnique(request.Username, cancellationToken))
        {
            return new Error("Username already exists");
        }

        var account = new Account(Guid.NewGuid(), request.Username, request.Password,request.Fullname, request.Role);

        switch (request.Role)
        {
            case Role.Student:
                var student = new Student(account.Id);
                await _studentRepository.AddAsync(student, cancellationToken);
                break;
            case Role.Instructor:
                var instructor = new Instructor(account.Id);
                await _instructorRepository.AddAsync(instructor, cancellationToken);
                break;
            default:
                return new Error($"Unknow type of {request.Role}");
        }

        await _accountRepository.AddAsync(account, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
