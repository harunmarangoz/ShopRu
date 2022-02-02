using Business.Interfaces;
using Business.Interfaces.Orders;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;

namespace WebApi.Controllers;

[Route("api/order")]
public class OrderController : BaseController
{
    private IOrderService _orderService;
    private IOrderItemService _orderItemService;

    public OrderController(IOrderService orderService, IOrderItemService orderItemService)
    {
        _orderService = orderService;
        _orderItemService = orderItemService;
    }

    [HttpGet]
    public IActionResult List()
    {
        return Ok(_orderService.List());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(_orderService.GetOrder(id));
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] CreateOrderDto model)
    {
        return Ok(_orderService.Create(model));
    }

    [HttpPost("add-order-item")]
    public IActionResult Create([FromBody] OrderItemDto model)
    {
        return Ok(_orderService.AddItem(model));
    }
}