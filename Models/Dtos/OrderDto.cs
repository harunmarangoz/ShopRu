using Models.Enums;

namespace Models.Dtos;

public class CreateOrderDto
{
    public long? UserId { get; set; }
    
    public double TotalProduct { get; set; }
    public double TotalDiscount { get; set; }
    public double Total { get; set; }

    public OrderType OrderType { get; set; }

    public IList<OrderItemDto> Items { get; private set; } = new List<OrderItemDto>();
}

public class GetOrderDto
{
    public long? UserId { get; set; }
    
    public double TotalProduct { get; set; }
    public double TotalDiscount { get; set; }
    public double Total { get; set; }

    public OrderType OrderType { get; set; }

    public IList<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
}