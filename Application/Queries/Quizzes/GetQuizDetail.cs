using Domain.Results;
using MediatR;

namespace Application.Queries.Quizzes;

public record GetQuizDetailCommand(Guid QuizId) : IRequest<Result<QuizResponse>>
{
}

public class GetQuizDetailCommandHandler(IQuizQuery quizQuery) : IRequestHandler<GetQuizDetailCommand, Result<QuizResponse>>
{
    private readonly IQuizQuery _quizQuery = quizQuery;

    public async Task<Result<QuizResponse>> Handle(GetQuizDetailCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _quizQuery.GetByIdAsync(request.QuizId, cancellationToken);
        if (quiz == null)
        {
            return new Error("Quiz not found");
        }
        return quiz;
    }
}
