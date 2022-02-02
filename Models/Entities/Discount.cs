using Core.Entities.Implementations;
using Models.Enums;

namespace Models.Entities;

public class Discount : Entity
{
    public long OrderId { get; set; }
    public Order Order { get; set; }
    
    public string Name { get; set; }
    
    public string AssemblyName { get; set; }
    public double Amount { get; set; }
    public DiscountType DiscountType { get; set; }
}