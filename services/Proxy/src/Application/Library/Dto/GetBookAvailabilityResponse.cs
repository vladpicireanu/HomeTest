using Application.Models;

namespace Application.Library.Dto
{
    public class GetBookAvailabilityResponse
    {
        public BookAvailability Book { get; set; } = null!;
    }
}
