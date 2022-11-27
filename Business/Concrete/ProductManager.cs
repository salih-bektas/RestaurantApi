using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Domain;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;

    public ProductManager(IProductDal productDal)
    {
        _productDal = productDal;
    }
    
    public Product CreateProduct(Product product)
    {
        var addedProduct = _productDal.Add(product);
        return addedProduct;
    }

    public IEnumerable<Product> GetProducts()
    {
        var products = _productDal.GetList();
        return products;
    }

    public int DeleteProduct(Product product)
    {
        return _productDal.Delete(product);
    }

    public Product? GetProductById(Guid id)
    {
        var product = _productDal.Get(t => t.Id == id);
        return product;
    }

    public Product Update(Product product)
    {
        return _productDal.Update(product);
    }
}