using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string  LastName { get; set; } = null!;

        public string? Email { get; set; }
    }
}
