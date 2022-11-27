using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace DataAccess.Domain;

public class Product : EntityBase
{
    public Product()
    {
        Sections = new List<Section>();
    }
    [Range(0, float.MaxValue, ErrorMessage = "Price should not be less than 0")]
    public float Price { get; set; }
    
    [StringLength(256)]
    public string Name { get; set; }
    
    [StringLength(1024)]
    public string Description { get; set; }
    
    [StringLength(256)]
    public string? SubTitle { get; set; }
    
    [StringLength(256)]
    public string? Amount { get; set; }

    public ICollection<Section> Sections { get; set; }
}