using Application.Models;

namespace Application.Library.Dto.Responses
{
    public class GetBookByIdResponse
    {
        public Book Book { get; set; } = null!;
    }
}
