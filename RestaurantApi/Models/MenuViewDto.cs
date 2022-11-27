namespace RestaurantApi.Models;

public class MenuViewDto
{
    public MenuViewDto()
    {
        Sections = new List<SectionViewDto>();
    }
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public ICollection<SectionViewDto> Sections { get; set; }
}