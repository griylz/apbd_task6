using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs;

using System.ComponentModel.DataAnnotations;

public class AddAnimal
{
    [Required]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
    [MaxLength(200, ErrorMessage = "Name must not exceed 200 characters.")]
    public string Name { get; set; }

    [MaxLength(200, ErrorMessage = "Description must not exceed 200 characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Category is required.")]
    [MaxLength(50, ErrorMessage = "Category must not exceed 50 characters.")]
    public string Category { get; set; }

    [Required(ErrorMessage = "Area is required.")]
    [MaxLength(50, ErrorMessage = "Area must not exceed 50 characters.")]
    public string Area { get; set; }
}
