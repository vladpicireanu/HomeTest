using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        public User()
        {
            this.Rents = new List<Rent>();
        }

        [Key]
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string  LastName { get; set; } = null!;

        public string? Email { get; set; }

        public virtual ICollection<Rent> Rents { get; private set; }
    }
}
