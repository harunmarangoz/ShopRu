using Core.Utilities.Results;
using Models.Entities;
using Models.Enums;

namespace Business.Implementations.Discounts;

public static class DiscountRules
{
    public static IResult AnyPercentageDiscount(Order order)
    {
        if (order.Discounts.Any(x => x.DiscountType == DiscountType.Percentage)) return new ErrorResult("Percentage discount can be applied once.");
        return new SuccessResult();
    }
    public static IResult MustNotGroceries(Order order)
    {
        if (order.Discounts.Any(x => x.DiscountType == DiscountType.Percentage)) return new ErrorResult("Percentage discount can be applied once.");
        return new SuccessResult();
    }
}