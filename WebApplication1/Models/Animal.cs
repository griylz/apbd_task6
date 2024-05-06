using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Animal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdAnimal { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [Required]
    [StringLength(50)]
    public string Category { get; set; }

    [Required]
    [StringLength(50)]
    public string Area { get; set; }
}