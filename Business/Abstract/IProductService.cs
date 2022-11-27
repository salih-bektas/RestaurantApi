using DataAccess.Domain;

namespace Business.Abstract;

public interface IProductService
{
    Product CreateProduct(Product product);
    IEnumerable<Product> GetProducts();
    int DeleteProduct(Product product);
    Product? GetProductById(Guid id);

    Product Update(Product product);
}