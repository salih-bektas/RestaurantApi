using System.ComponentModel.DataAnnotations;
using DataAccess.Domain;

namespace RestaurantApi.Models;

public class OrderDto
{
    public OrderDto()
    {
        Products = new List<Guid>();
    }
    
    [Required]
    public DateTime OrderDate { get; set; }
    public ICollection<Guid> Products { get; set; }
}