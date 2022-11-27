using AutoMapper;
using Business.Abstract;
using DataAccess.Domain;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models;

namespace RestaurantApi.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;

    public OrderController(IMapper mapper,
        IOrderService orderService,
        IProductService productService)
    {
        _mapper = mapper;
        _orderService = orderService;
        _productService = productService;
    }
    
    //Order operations
    
    /// <summary>
    /// Get list or Orders
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<OrderViewDto> Get()
    {
        return _orderService.GetOrders().Select(c => _mapper.Map<OrderViewDto>(c));
    }
    
    /// <summary>
    /// Get Order by an Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Order</returns>
    [HttpGet("{id:Guid}")]
    public ActionResult Get(Guid id)
    {
        var order = _orderService.GetOrderById(id);
        if (order == null)
        {
            return NotFound($"Order with Id {id} not found");
        }

        return Ok(_mapper.Map<OrderViewDto>(order));
    }
    
    /// <summary>
    /// Creates order for a given date and products
    /// </summary>
    /// <param name="orderDto"></param>
    /// <returns>Order</returns>
    [HttpPost]
    public ActionResult CreateOrder(OrderDto orderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var order = new Order()
        {
            OrderDate = orderDto.OrderDate
        };
        foreach (var productId in orderDto.Products)
        {
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                return NotFound($"Product with Id {productId} not found");
            }
            order.Products.Add(product);
        }

        var createdOrder = _orderService.CreateOrder(order);
        return Ok(_mapper.Map<OrderViewDto>(createdOrder));
    }
}