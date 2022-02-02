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
public class EmployeeTest : IClassFixture<InjectionFixture>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private IOrderService _orderService;
    private IUserService _userService;

    private readonly InjectionFixture _injection;

    public EmployeeTest(InjectionFixture injection, ITestOutputHelper testOutputHelper)
    {
        _injection = injection;
        _testOutputHelper = testOutputHelper;
        _orderService = _injection.ServiceProvider.GetRequiredService<IOrderService>();
        _userService = _injection.ServiceProvider.GetRequiredService<IUserService>();
    }

    [Fact]
    [TestPriority(1)]
    public void CreateEmployee()
    {
        var user = _userService.Create(new UserDto()
        {
            FirstName = "Employee",
            LastName = "User",
            UserName = "employee",
            Role = RoleEnum.Employee,
            Password = "123456"
        });

        Assert.True(user.Success);
    }

    [Fact, TestPriority(2)]
    public void CreateEmployeeUnder100Order()
    {
        var user = _userService.GetByRoleFirst(RoleEnum.Employee);
        var dto = new CreateOrderDto();
        dto.UserId = user.Data.Id;
        dto.OrderType = OrderType.Market;
        dto.Items.Add(new() {ProductName = "Book", ProductPrice = 50});
        var result = _orderService.Create(dto);

        Assert.True(result.Success);
        Assert.Equal(35, result.Data.Total);
        Assert.Equal(15, result.Data.TotalDiscount);
    }

    [Fact, TestPriority(3)]
    public void CreateEmployeeOrderOver100()
    {
        var user = _userService.GetByRoleFirst(RoleEnum.Employee);
        var dto = new CreateOrderDto();
        dto.UserId = user.Data.Id;
        dto.OrderType = OrderType.Market;
        dto.Items.Add(new() {ProductName = "Book", ProductPrice = 1000});
        var result = _orderService.Create(dto);

        _testOutputHelper.WriteLine(
            $"order created: #{result.Data.Id} Total: {result.Data.Total} {result.Data.Discounts.Count} {result.Data.Items.Count}  {DateTime.Now.ToString("O")}");

        Assert.True(result.Success);
        Assert.Equal(665, result.Data.Total);
        Assert.Equal(335, result.Data.TotalDiscount);
    }
}