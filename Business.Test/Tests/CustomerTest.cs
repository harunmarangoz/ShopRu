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
public class CustomerTest : IClassFixture<InjectionFixture>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private IOrderService _orderService;
    private IUserService _userService;

    private User customer;

    private readonly InjectionFixture _injection;

    public CustomerTest(InjectionFixture injection, ITestOutputHelper testOutputHelper)
    {
        _injection = injection;
        _testOutputHelper = testOutputHelper;
        _orderService = _injection.ServiceProvider.GetRequiredService<IOrderService>();
        _userService = _injection.ServiceProvider.GetRequiredService<IUserService>();
    }

    [Fact]
    [TestPriority(1)]
    public void CreateCustomer()
    {
        var user = _userService.Create(new UserDto()
        {
            FirstName = "Customer",
            LastName = "User",
            UserName = "customer",
            Role = RoleEnum.Customer,
            Password = "123456"
        });

        _testOutputHelper.WriteLine(
            $"customer created: #{user.Data.Id} {user.Data.FirstName} {user.Data.LastName}  {DateTime.Now.ToString("O")}");

        Assert.True(user.Success);

        customer = user.Data;
    }
    

    [Fact, TestPriority(2)]
    public void CreateCustomerOrderOver100()
    {
        var user = _userService.GetByRoleFirst(RoleEnum.Customer);

        var dto = new CreateOrderDto();
        dto.UserId = user.Data.Id;
        dto.OrderType = OrderType.Market;
        dto.Items.Add(new() {ProductName = "Book", ProductPrice = 1000});
        var result = _orderService.Create(dto);

        Assert.True(result.Success);
        Assert.Equal(950, result.Data.Total);
        Assert.Equal(50, result.Data.TotalDiscount);
    }

    [Fact, TestPriority(3)]
    public void CreateCustomerUnder100OrderWithPastOrders()
    {
        var user = _userService.GetByRoleFirst(RoleEnum.Customer);

        _testOutputHelper.WriteLine($"User found: #{user.Data.Id} {user.Data.Role}");

        var dto = new CreateOrderDto();
        dto.UserId = user.Data.Id;
        dto.OrderType = OrderType.Market;
        dto.Items.Add(new() {ProductName = "Book", ProductPrice = 50});
        var result = _orderService.Create(dto);

        _testOutputHelper.WriteLine(
            $"order created: #{result.Data.Id} Total: {result.Data.Total} {result.Data.Discounts.Count} {result.Data.Items.Count}  {DateTime.Now.ToString("O")}");

        Assert.True(result.Success);
        Assert.Equal(47.5, result.Data.Total);
        Assert.Equal(2.5, result.Data.TotalDiscount);
    }

    [Fact, TestPriority(4)]
    public void CreateCustomerOrderOver100WithPastOrder()
    {
        var user = _userService.GetByRoleFirst(RoleEnum.Customer);

        _testOutputHelper.WriteLine($"User found: #{user.Data.Id} {user.Data.Role}");

        var dto = new CreateOrderDto();
        dto.UserId = user.Data.Id;
        dto.OrderType = OrderType.Market;
        dto.Items.Add(new() {ProductName = "Book", ProductPrice = 1000});
        var result = _orderService.Create(dto);

        _testOutputHelper.WriteLine(
            $"order created: #{result.Data.Id} Total: {result.Data.Total} {result.Data.Discounts.Count} {result.Data.Items.Count}  {DateTime.Now.ToString("O")}");

        Assert.True(result.Success);
        Assert.Equal(902.5, result.Data.Total);
        Assert.Equal(97.5, result.Data.TotalDiscount);
    }
}