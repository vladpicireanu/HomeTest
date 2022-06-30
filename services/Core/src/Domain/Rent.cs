using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Rent
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public DateTimeOffset RentStartDate { get; set; }

        public DateTimeOffset? RentStopDate { get; set; }
    }
}
