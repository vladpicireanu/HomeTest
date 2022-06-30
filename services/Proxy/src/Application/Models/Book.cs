namespace Application.Models
{
    public class Book
    {
        public int BookId { get; set; }

        public string Name { get; set; } = null!;

        public int Pages { get; set; }

        public int Copies { get; set; }
    }
}
