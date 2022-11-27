using System.ComponentModel.DataAnnotations;

namespace DataAccess.Domain;

public class Menu : EntityBase
{
    public Menu()
    {
        Sections = new List<Section>();
    }
    [Required]
    public DateTime Date { get; set; }
    public List<Section> Sections { get; set; }
}