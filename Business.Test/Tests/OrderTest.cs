using Business.Interfaces.Orders;
using Business.Interfaces.Users;
using Core.Tests;
using Core.Utilities.IoC;
using Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Dtos;
using Models.Enums;
using Xunit;
using Xunit.Abstractions;

namespace Business.Test.Tests;

[TestCaseOrderer("Core.Tests.TestPriorityOrderer", "Core")]
public class OrderTest : IClassFixture<InjectionFixture>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private IOrderService _orderService;
    private IUserService _userService;

    private readonly InjectionFixture _injection;

    public OrderTest(InjectionFixture injection, ITestOutputHelper testOutputHelper)
    {
        _injection = injection;
        _testOutputHelper = testOutputHelper;
        _orderService = _injection.ServiceProvider.GetRequiredService<IOrderService>();
        _userService = _injection.ServiceProvider.GetRequiredService<IUserService>();
    }

    [Fact, TestPriority(201)]
    public void CreateOrderUnder100()
    {
        var dto = new CreateOrderDto();
        dto.OrderType = OrderType.Market;
        dto.Items.Add(new() {ProductName = "Book", ProductPrice = 50});
        var result = _orderService.Create(dto);

        _testOutputHelper.WriteLine(
            $"order created: #{result.Data.Id} Total: {result.Data.Total} {result.Data.Discounts.Count} {result.Data.Items.Count}  {DateTime.Now.ToString("O")}");

        Assert.True(result.Success);
        Assert.Equal(50, result.Data.Total);
        Assert.Equal(0, result.Data.TotalDiscount);
    }

    [Fact, TestPriority(202)]
    public void CreateOrderOver100()
    {
        var dto = new CreateOrderDto();
        dto.OrderType = OrderType.Market;
        dto.Items.Add(new() {ProductName = "Book", ProductPrice = 1000});
        var result = _orderService.Create(dto);

        _testOutputHelper.WriteLine(
            $"order created: #{result.Data.Id} Total: {result.Data.Total} {result.Data.Discounts.Count} {result.Data.Items.Count}  {DateTime.Now.ToString("O")}");

        Assert.True(result.Success);
        Assert.Equal(950, result.Data.Total);
        Assert.Equal(50, result.Data.TotalDiscount);
    }

    


}