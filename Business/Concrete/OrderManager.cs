using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete;

public class OrderManager : IOrderService
{
    private readonly IOrderDal _orderDal;

    public OrderManager(IOrderDal orderDal)
    {
        _orderDal = orderDal;
    }
    public Order CreateOrder(Order order)
    {
        var addedOrder = _orderDal.Add(order);
        return addedOrder;
    }

    public IEnumerable<Order> GetOrders()
    {
        return _orderDal.Table.Include(t => t.Products);
    }

    public Order? GetOrderById(Guid id)
    {
        return _orderDal.Table.Where(t => t.Id == id)
            .Include(t => t.Products).FirstOrDefault();
    }

    public IEnumerable<Order> GetOrderByDate(DateTime date)
    {
        return _orderDal.Table.Where(t => t.OrderDate == date.Date)
            .Include(t => t.Products);
    }
}