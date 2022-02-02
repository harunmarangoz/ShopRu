using Business.Implementations.Discounts;
using Business.Implementations.Orders;
using Business.Implementations.Users;
using Business.Interfaces.Discounts;
using Business.Interfaces.Orders;
using Business.Interfaces.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Configurations;

public static class BusinessConfiguration
{
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddTransient<IUserService, UserManager>();
        services.AddTransient<IOrderService, OrderManager>();
        services.AddTransient<IOrderItemService, OrderItemManager>();

        services.AddTransient<IDiscountService, DiscountManager>();
        services.AddTransient<IDiscountRuleService, EmployeeDiscountRuleService>();
        services.AddTransient<IDiscountRuleService, AffiliateDiscountRuleService>();
        services.AddTransient<IDiscountRuleService, Every100DiscountRuleService>();
        services.AddTransient<IDiscountRuleService, PastOrdersDiscountRuleService>();
    }
}