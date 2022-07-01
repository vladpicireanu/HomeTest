using Application.Models;

namespace Application.Library.Dto.Responses
{
    public class GetOtherBooksResponse
    {
        public List<Book> Books { get; set; } = null!;
    }
}
