using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Book
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int Pages { get; set; }

        [Required]
        public int Copies { get; set; }
    }
}
