using System.ComponentModel.DataAnnotations;

namespace DataAccess.Domain;

public class Section : EntityBase
{
    public Section()
    {
        Products = new List<Product>();
    }
    [StringLength(256)]
    public string Title { get; set; }
    
    public ICollection<Product> Products { get; set; }
    
    public Guid MenuId { get; set; }
}