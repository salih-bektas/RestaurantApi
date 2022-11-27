using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using RestaurantApi.Converters;

namespace RestaurantApi.Models;

public class MenuDto
{
    public MenuDto()
    {
        Sections = new List<Guid>();
    }
    
    [Required]
    public DateTime Date { get; set; }
    
    public ICollection<Guid> Sections { get; set; }
}