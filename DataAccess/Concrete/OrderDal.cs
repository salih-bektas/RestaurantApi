using DataAccess.Abstract;
using DataAccess.Domain;
using DataAccess.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete;

public class OrderDal : Repository<Order>, IOrderDal
{
    public OrderDal(DbContext context) : base(context) {}
}