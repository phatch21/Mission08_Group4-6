using System.ComponentModel.DataAnnotations;

namespace Mission08_Group4_6.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TaskName { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "Quadrant must be between 1 and 4.")]
        public int Quadrant { get; set; }

        [Required]
        [EnumDataType(typeof(TaskCategory))]
        public TaskCategory Category { get; set; }

        [Required]
        public bool Completed { get; set; } = false;
    }

    public enum TaskCategory
    {
        Home,
        School,
        Work,
        Church
    }
}
