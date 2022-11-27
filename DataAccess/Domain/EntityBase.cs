using System.ComponentModel.DataAnnotations;

namespace DataAccess.Domain;

public class EntityBase
{
    [Required]
    [Key]
    public Guid Id { get; set; }
}