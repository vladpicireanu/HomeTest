using Application.Models;

namespace Application.Library.Dto.Responses
{
    public class GetBookAvailabilityResponse
    {
        public BookAvailability Book { get; set; } = null!;
    }
}
