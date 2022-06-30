using Application.Models;

namespace Application.Library.Dto
{
    public class GetBookByIdResponse
    {
        public BookModel Book { get; set; } = null!;
    }
}
