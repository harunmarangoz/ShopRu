using Core.Utilities.Results;
using Models.Entities;

namespace Business.Interfaces.Discounts;

public interface IDiscountRuleService
{
    IDataResult<Discount> GetDiscount(Order order);
}