using Application.Commands.Quizzes;
using Application.Queries.Quizzes;
using Domain.Aggregates.QuizAggregate;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.JsonConverter;

public class QuestionResponseConverter : JsonConverter<QuestionResponse>
{
    public override QuestionResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        // Kiểm tra xem type là gì
        var type = root.GetProperty("type").GetString();
        if (!Enum.TryParse<QuestionType>(type, ignoreCase: true, out var questionType))
        {
            throw new JsonException($"Invalid question type: {type}");
        }

        QuestionResponse question = questionType switch
        {
            QuestionType.MultipleChoice => JsonSerializer.Deserialize<MultipleChoiceQuestionResponse>(root.GetRawText(), options)
                    ?? throw new JsonException("Failed to deserialize MultipleChoiceQuestionResponse"),

            QuestionType.ShortAnswer => JsonSerializer.Deserialize<ShortAnswerQuestionResponse>(root.GetRawText(), options)
                    ?? throw new JsonException("Failed to deserialize ShortAnswerQuestionRResponse"),

            _ => throw new NotSupportedException($"Type {type} is not supported")
        };

        return question;
    }
    public override void Write(Utf8JsonWriter writer, QuestionResponse value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
