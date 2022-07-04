namespace Presentation.Response
{
    public class GenericErrorPayload
    {
        public string Status { get; set; } = null!;

        public string Message { get; set; } = null!;

        public string Code { get; set; } = null!;
    }
}
