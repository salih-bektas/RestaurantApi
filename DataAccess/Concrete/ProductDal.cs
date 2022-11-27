using DataAccess.Abstract;
using DataAccess.Domain;
using DataAccess.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete;

public class ProductDal : Repository<Product>, IProductDal
{
    public ProductDal(DbContext context) : base(context) {}
}