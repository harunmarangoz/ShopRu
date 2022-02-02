using Core.Entities.Implementations;
using Models.Enums;

namespace Models.Entities;

public class Order : Entity
{
    public long? UserId { get; set; }
    public User User { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public double TotalProduct { get; set; }
    public double TotalDiscount { get; set; }
    public double Total { get; set; }

    public OrderType OrderType { get; set; }

    public List<OrderItem> Items { get; private set; } = new();
    public List<Discount> Discounts { get; private set; } = new();
}