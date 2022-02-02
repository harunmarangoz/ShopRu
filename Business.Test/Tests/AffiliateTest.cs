using Business.Interfaces.Orders;
using Business.Interfaces.Users;
using Core.Tests;
using Microsoft.Extensions.DependencyInjection;
using Models.Dtos;
using Models.Entities;
using Models.Enums;
using Xunit;
using Xunit.Abstractions;

namespace Business.Test.Tests;

[TestCaseOrderer("Core.Tests.TestPriorityOrderer", "Core")]
public class AffiliateTest: IClassFixture<InjectionFixture>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private IOrderService _orderService;
    private IUserService _userService;

    private User affiliate;

    private readonly InjectionFixture _injection;

    public AffiliateTest(InjectionFixture injection, ITestOutputHelper testOutputHelper)
    {
        _injection = injection;
        _testOutputHelper = testOutputHelper;
        _orderService = _injection.ServiceProvider.GetRequiredService<IOrderService>();
        _userService = _injection.ServiceProvider.GetRequiredService<IUserService>();
    }

    [Fact]
    [TestPriority(1)]
    public void CreateAffiliate()
    {
        var user = _userService.Create(new UserDto()
        {
            FirstName = "Affiliate",
            LastName = "User",
            UserName = "affiliate",
            Role = RoleEnum.Affiliate,
            Password = "123456"
        });

        Assert.True(user.Success);
    }
    
    [Fact, TestPriority(2)]
    public void CreateAffiliateUnder100Order()
    {
        var user = _userService.GetByRoleFirst(RoleEnum.Affiliate);

        var dto = new CreateOrderDto();
        dto.UserId = user.Data.Id;
        dto.OrderType = OrderType.Market;
        dto.Items.Add(new() {ProductName = "Book", ProductPrice = 50});
        var result = _orderService.Create(dto);

        Assert.True(result.Success);
        Assert.Equal(45, result.Data.Total);
        Assert.Equal(5, result.Data.TotalDiscount);
    }

    [Fact, TestPriority(3)]
    public void CreateAffiliateOrderOver100()
    {
        var user = _userService.GetByRoleFirst(RoleEnum.Affiliate);

        var dto = new CreateOrderDto();
        dto.UserId = user.Data.Id;
        dto.OrderType = OrderType.Market;
        dto.Items.Add(new() {ProductName = "Book", ProductPrice = 1000});
        var result = _orderService.Create(dto);

        Assert.True(result.Success);
        Assert.Equal(855, result.Data.Total);
        Assert.Equal(145, result.Data.TotalDiscount);
    }
}