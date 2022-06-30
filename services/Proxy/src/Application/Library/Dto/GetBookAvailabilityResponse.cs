namespace Application.Library.Dto
{
    public class GetBookAvailabilityResponse
    {
        public int BookId { get; set; }

        public string Name { get; set; } = null!;

        public int Borrowed { get; set; }

        public int Available { get; set; }
    }
}
