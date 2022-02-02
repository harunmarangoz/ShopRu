namespace Models.Dtos;

public class OrderItemDto
{
    public long OrderId { get; set; }
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }
}