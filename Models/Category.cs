using System.ComponentModel.DataAnnotations;

namespace Mission08_Group4_6.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    
    [Required]
    public string CategoryName { get; set; }
    
    [Required]
    public int Id { get; set; }
    public NewTask NewTask { get; set; }
    
}