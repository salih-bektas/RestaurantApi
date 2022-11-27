using DataAccess.Domain;

namespace RestaurantApi.Models;

public class OrderViewDto
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<ProductViewDto> Products { get; set; }
}