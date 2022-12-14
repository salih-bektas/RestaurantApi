namespace RestaurantApi.Models;

public class ProductViewDto
{
    public Guid Id { get; set; }
    public float Price { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? SubTitle { get; set; }
    public string? Amount { get; set; }
}