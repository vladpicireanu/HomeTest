using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Book
    {
        public Book()
        {
            this.Rents = new List<Rent>();
        }

        [Key]
        [Required]
        public int BookId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int Pages { get; set; }

        [Required]
        public int Copies { get; set; }

        public virtual ICollection<Rent> Rents { get; private set; }
    }
}
