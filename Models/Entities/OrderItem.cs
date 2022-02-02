using Core.Entities.Implementations;

namespace Models.Entities;

public class OrderItem : Entity
{
    public long OrderId { get; set; }
    public Order Order { get; set; }
    
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }
}