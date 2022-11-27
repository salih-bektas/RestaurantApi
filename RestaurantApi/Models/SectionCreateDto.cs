using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models;

public class SectionCreateDto
{
    public SectionCreateDto()
    {
        Products = new List<Guid>();
    }
    
    [StringLength(256, ErrorMessage = "The field {0} must be between less than {1} characters")]
    public string Title { get; set; }
    
    public ICollection<Guid> Products { get; set; }
}