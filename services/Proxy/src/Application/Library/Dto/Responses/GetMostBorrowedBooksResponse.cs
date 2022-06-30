using Application.Models;

namespace Application.Library.Dto.Responses
{
    public class GetMostBorrowedBooksResponse
    {
        public List<Book> Books { get; set; } = null!;
    }
}
