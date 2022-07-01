using Application.Models;

namespace Application.Library.Dto
{
    public class GetOtherBooksResponse
    {
        public List<BookModel> Books { get; set; } = null!;
    }
}
