using Application.Models;

namespace Application.Library.Dto
{
    public class GetBookByIdResponse
    {
        public Book Book { get; set; } = null!;
    }
}
