using System.Text.Json.Serialization;

namespace Presentation.Response
{
    public class BadRequestValidationErrorPayload : GenericErrorPayload
    {
        [JsonPropertyOrder(order: 5)]
        public IList<ValidationErrorElement> Errors { get; set; } = null!;
    }

    public class ValidationErrorElement
    {
        public string Code { get; set; } = null!;
        public string Field { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
