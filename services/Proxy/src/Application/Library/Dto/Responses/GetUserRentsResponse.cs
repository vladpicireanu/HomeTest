using Application.Models;

namespace Application.Library.Dto.Responses
{
    public class GetUserRentsResponse
    {
        public List<UserRent> UserRents { get; set; } = null!;
    }
}
