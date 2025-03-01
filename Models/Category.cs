
using System.ComponentModel.DataAnnotations;

namespace Mission08_Group4_6.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty; // Ensures no null issues
    }

}

