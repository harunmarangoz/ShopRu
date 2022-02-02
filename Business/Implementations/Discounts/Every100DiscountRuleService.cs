using Business.Interfaces.Discounts;
using Core.Utilities.Results;
using Models.Entities;
using Models.Enums;

namespace Business.Implementations.Discounts;

public class Every100DiscountRuleService : IDiscountRuleService
{
    public IDataResult<Discount> GetDiscount(Order order)
    {
        if (order.Total < 100) return new ErrorDataResult<Discount>("Order total under 100 $");

        var amount = (int) (order.Total / 100) * 5;

        return new SuccessDataResult<Discount>(new Discount()
        {
            OrderId = order.Id,
            Name = "Affiliate %10",
            AssemblyName = GetType().Name,
            Amount = amount,
            DiscountType = DiscountType.Amount
        });
    }
}