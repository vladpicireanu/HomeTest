using Application.Models;

namespace Application.Library.Dto
{
    public class GetMostBorrowedBooksResponse
    {
        public List<Book> Books { get; set; } = null!;
    }
}
