using Application.Commands.Quizzes;
using Domain.Aggregates.QuizAggregate;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.JsonConverter;

public class QuestionRequestConverter : JsonConverter<QuestionRequest>
{
    public override QuestionRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        // Kiểm tra xem type là gì
        var type = root.GetProperty("type").GetString();

        if (!Enum.TryParse<QuestionType>(type, ignoreCase: true, out var questionType))
        {
            throw new JsonException($"Invalid question type: {type}");
        }
        QuestionRequest question = questionType switch
        {
            QuestionType.MultipleChoice => JsonSerializer.Deserialize<MultipleChoiceQuestionRequest>(root.GetRawText(), options)
                    ?? throw new JsonException("Failed to deserialize MultipleChoiceQuestionRequest"),

            QuestionType.ShortAnswer => JsonSerializer.Deserialize<ShortAnswerQuestionRequest>(root.GetRawText(), options)
                    ?? throw new JsonException("Failed to deserialize ShortAnswerQuestionRequest"),

            _ => throw new NotSupportedException($"Type {type} is not supported")
        };
        return question;
    }
    public override void Write(Utf8JsonWriter writer, QuestionRequest value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
