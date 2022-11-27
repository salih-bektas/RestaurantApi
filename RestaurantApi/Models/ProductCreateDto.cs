namespace RestaurantApi.Models;

public class ProductCreateDto
{
    public float Price { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? SubTitle { get; set; }
    public string? Amount { get; set; }
}