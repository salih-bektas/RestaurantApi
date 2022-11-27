namespace DataAccess.Domain;

public class Order : EntityBase
{
    public Order()
    {
        Products = new List<Product>();
    }
    public DateTime OrderDate { get; set; }
    public ICollection<Product> Products { get; set; }
}