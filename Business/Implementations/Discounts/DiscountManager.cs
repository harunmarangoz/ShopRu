using Business.Interfaces.Discounts;
using Core.Business.Implementations;
using Core.Utilities.Results;
using Data.Interfaces;
using Models.Entities;

namespace Business.Implementations.Discounts;

public class DiscountManager : EntityManager<Discount>, IDiscountService
{
    public DiscountManager(IDiscountDal orderDal) : base(orderDal)
    {
    }

    public IDataResult<Discount> Create(Discount discount)
    {
        var result = _entityRepository.Create(discount);
        return new SuccessDataResult<Discount>(result);
    }

    public void ClearDiscounts(long orderId)
    {
        var discounts = _entityRepository.List(x => x.OrderId == orderId);
        for (int i = 0; i < discounts.Count; i++)
        {
            _entityRepository.Delete(discounts[i]);
        }
    }
}