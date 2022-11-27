namespace RestaurantApi.Models;

public class SectionUpdateDto
{
    public string Title { get; set; }
    public ICollection<Guid> Products { get; set; }
}