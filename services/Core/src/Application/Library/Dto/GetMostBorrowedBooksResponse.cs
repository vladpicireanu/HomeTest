using Application.Models;

namespace Application.Library.Dto
{
    public class GetMostBorrowedBooksResponse
    {
        public List<BookModel> Books { get; set; } = null!;
    }
}
