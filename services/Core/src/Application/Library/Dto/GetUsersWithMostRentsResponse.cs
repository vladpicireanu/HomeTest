using Application.Models;

namespace Application.Library.Dto
{
    public class GetUsersWithMostRentsResponse
    {
        public List<UserMostRents> Users { get; set; } = null!;
    }
}
