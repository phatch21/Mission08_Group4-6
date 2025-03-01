using Mission08_Group4_6.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class NewTask
{
    public NewTask()
    {
        CategoryId = 0; // Ensure it has a default value
        TaskName = string.Empty; // Prevents null issues
    }

    [Key]
    public int Id { get; set; }

    [Required]
    public string TaskName { get; set; } = string.Empty; // Ensures it is never null

    public DateTime? DueDate { get; set; }

    [Required]
    [Range(1, 4, ErrorMessage = "Quadrant must be between 1 and 4.")]
    public int Quadrant { get; set; }

    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; } // Made nullable to prevent binding issues

    [Required]
    public bool Completed { get; set; } = false;
}
