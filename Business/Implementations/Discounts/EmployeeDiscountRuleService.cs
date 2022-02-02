using System.Diagnostics.CodeAnalysis;
using Business.Interfaces.Discounts;
using Core.Utilities.Results;
using Data.Interfaces;
using Models.Entities;
using Models.Enums;

namespace Business.Implementations.Discounts;

public class EmployeeDiscountRuleService : IDiscountRuleService
{
    IUserDal _userDal;

    public EmployeeDiscountRuleService(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public IDataResult<Discount> GetDiscount(Order order)
    {
        var result = BusinessRules.Run(DiscountRules.AnyPercentageDiscount(order), DiscountRules.MustNotGroceries(order));
        
        if (result != null) return new ErrorDataResult<Discount>(result.Message);
        
        if (!order.UserId.HasValue) return new ErrorDataResult<Discount>("Order not has user.");

        var user =  _userDal.Get(x => x.Id == order.UserId.Value);

        if (user == null) return new ErrorDataResult<Discount>("User not found");

        if (user.Role != RoleEnum.Employee) return new ErrorDataResult<Discount>("User is not employee");

        return new SuccessDataResult<Discount>(new Discount()
        {
            OrderId = order.Id,
            Name = "Employee %30",
            AssemblyName = GetType().Name,
            Amount = order.Total * 0.30,
            DiscountType = DiscountType.Percentage
        });
    }
}