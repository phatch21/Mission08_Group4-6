using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission08_Group4_6.Models
{
    public class NewTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TaskName { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "Quadrant must be between 1 and 4.")]
        public int Quadrant { get; set; }

        // CategoryId as a foreign key to Categories table
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        // Navigation property to Category (optional)
        public Category Category { get; set; }

        [Required]
        public bool Completed { get; set; } = false;
    }
}
