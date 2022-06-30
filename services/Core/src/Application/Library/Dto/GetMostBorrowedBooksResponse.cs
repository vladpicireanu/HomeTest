using Application.Models;

namespace Application.Library.Dto
{
    public class GetMostBorrowedBooksResponse
    {
        public List<BookModel> MostBorrowedBooks { get; set; } = null!;
    }
}
