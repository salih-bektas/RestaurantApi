namespace RestaurantApi.Models;

public class SectionViewDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public ICollection<ProductViewDto> Products { get; set; }
}