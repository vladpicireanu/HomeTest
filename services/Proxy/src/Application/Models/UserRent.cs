namespace Application.Models
{
    public class UserRent
    {
        public int BookId { get; set; }

        public string Name { get; set; } = null!;

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset? ReturnDate { get; set; }
    }
}
