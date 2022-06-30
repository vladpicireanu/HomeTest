using Application.Models;

namespace Application.Library.Dto.Responses
{
    public class GetUsersWithMostRentsResponse
    {
        public List<UserMostRents> Users { get; set; } = null!;
    }
}
