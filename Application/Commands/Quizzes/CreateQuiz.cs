using Domain.Aggregates.QuizAggregate;
using Domain.Aggregates.QuizAggregate.MultipleChoiceQuestionEntity;
using Domain.Aggregates.QuizAggregate.ShortAnswerQuestionEntity;
using Domain.IRepository;
using Domain.Results;
using MediatR;

namespace Application.Commands.Quizzes;

public record CreateQuizCommand(string Title, string Description, IEnumerable<QuestionRequest> Questions) : IRequest<Result>
{
}

public class CreateQuizCommandHandler(
    IQuizRepository quizRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateQuizCommand, Result>
{
    private readonly IQuizRepository _quizRepository = quizRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        List<Question> questions = [];
        foreach (var question in request.Questions)
        {
            switch (question)
            {
                case MultipleChoiceQuestionRequest mcqRequest:
                    var choices = mcqRequest.Choices.Select(c => (c.Text, c.IsCorrect)).ToArray();

                    var multipleChoiceQuestion = MultipleChoiceQuestion.Create(
                        Guid.NewGuid(),
                        mcqRequest.Title,
                        mcqRequest.Order,
                        choices
                        );

                    questions.Add(multipleChoiceQuestion);
                    break;

                case ShortAnswerQuestionRequest shortAnswerRequest:
                    var shortAnswerQuestion = ShortAnswerQuestion.Create(
                        Guid.NewGuid(),
                        shortAnswerRequest.Title,
                        shortAnswerRequest.Order,
                        [.. shortAnswerRequest.CorrectAnswers]
                        );

                    questions.Add(shortAnswerQuestion);
                    break;

                default:
                    throw new NotSupportedException($"Question type {question.GetType()} is not supported.");
            }
        }

        var quiz = Quiz.Create(
            Guid.NewGuid(),
            request.Title,
            request.Description,
            [.. questions]
            );

        await _quizRepository.AddAsync(quiz, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}