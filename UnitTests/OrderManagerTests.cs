using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;

namespace UnitTests;

public class OrderManagerTests
{
    private readonly Mock<IOrderDal> _mockOrderDal;
    private readonly Fixture _fixture;

    public OrderManagerTests()
    {
        _fixture = new Fixture();
        _mockOrderDal = _fixture.Freeze<Mock<IOrderDal>>();
    }
    
    [Fact]
    public void GetOrderById_When_IdIsWrong_Return_Null()
    {
        //Arrange
        var orderId = _fixture.Create<Guid>();
        var orders = new List<Order>()
        {
            new Order
            {
                Id = orderId,
                Products = { }
            }
        };
        _mockOrderDal.Setup(t => t.Table).Returns(orders.AsQueryable);
        var orderManager = new OrderManager(_mockOrderDal.Object);
        
        //Act
        var result = orderManager.GetOrderById(new Guid());
        
        //Assert
        Assert.Null(result);
    }
}