namespace Application.Models
{
    public class UserMostRents
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Email { get; set; }

        public int Rents { get; set; }
    }
}
