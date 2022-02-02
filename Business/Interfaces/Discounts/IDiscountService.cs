using Core.Business.Interfaces;
using Core.Utilities.Results;
using Models.Entities;

namespace Business.Interfaces.Discounts;

public interface IDiscountService : IEntityService<Discount>
{
    IDataResult<Discount> Create(Discount discount);
    void ClearDiscounts(long orderId);
}