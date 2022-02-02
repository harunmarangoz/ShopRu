using Business.Interfaces.Discounts;
using Core.Utilities.Results;
using Data.Interfaces;
using Models.Entities;
using Models.Enums;

namespace Business.Implementations.Discounts;

public class PastOrdersDiscountRuleService : IDiscountRuleService
{
    IUserDal _userDal;
    private IOrderDal _orderDal;
    
    public PastOrdersDiscountRuleService(IUserDal userDal, IOrderDal orderDal)
    {
        _userDal = userDal;
        _orderDal = orderDal;
    }

    public IDataResult<Discount> GetDiscount(Order order)
    {
        var result = BusinessRules.Run(DiscountRules.AnyPercentageDiscount(order), DiscountRules.MustNotGroceries(order));
        
        if (result != null) return new ErrorDataResult<Discount>(result.Message);
        
        if (!order.UserId.HasValue) return new ErrorDataResult<Discount>("Order not has user.");

        var user =  _userDal.Get(x => x.Id == order.UserId.Value);

        if (user == null) return new ErrorDataResult<Discount>("User not found");

        var date = DateTime.Now.AddYears(-2);

        var pastOrder = _orderDal.Get(x => x.UserId == order.UserId && x.OrderDate.Date > date.Date && x.Id != order.Id);

        if (pastOrder == null) return new ErrorDataResult<Discount>("No past order");

        return new SuccessDataResult<Discount>(new Discount()
        {
            OrderId = order.Id,
            Name = "Past Orders %5",
            AssemblyName = GetType().Name,
            Amount = order.Total * 0.05,
            DiscountType = DiscountType.Percentage
        });
    }
}