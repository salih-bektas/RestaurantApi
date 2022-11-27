using DataAccess.Domain;

namespace Business.Abstract;

public interface IOrderService
{
    Order CreateOrder(Order order);
    IEnumerable<Order> GetOrders();
    Order? GetOrderById(Guid id);
    IEnumerable<Order> GetOrderByDate(DateTime date);
}