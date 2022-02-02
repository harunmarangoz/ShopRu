using System.Data.Entity;
using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Discounts;
using Business.Interfaces.Orders;
using Core.Business.Implementations;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Models.Dtos;
using Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Business.Implementations.Orders;

public class OrderManager : EntityManager<Order>, IOrderService
{
    private IMapper _mapper;
    private IOrderDal _orderDal;
    private IEnumerable<IDiscountRuleService> _discountRuleServices;
    private IDiscountService _discountService;
    private IOrderItemService _orderItemService;

    public OrderManager(IOrderDal orderDal, IMapper mapper, IEnumerable<IDiscountRuleService> discountRuleServices,
        IDiscountService discountService, IOrderItemService orderItemService) : base(orderDal)
    {
        _mapper = mapper;
        _orderDal = orderDal;
        _discountRuleServices = discountRuleServices;
        _discountService = discountService;
        _orderItemService = orderItemService;
    }

    public IDataResult<List<Order>> List()
    {
        return new SuccessDataResult<List<Order>>(_entityRepository.List());
    }

    public IDataResult<Order> Create(CreateOrderDto dto)
    {
        var result = _entityRepository.Create(new ()
        {
            OrderType = dto.OrderType,
            UserId = dto.UserId
        });

        foreach (var dtoItem in dto.Items)
        {
            dtoItem.OrderId = result.Id;
            _orderItemService.Create(dtoItem);
        }

        Calculate(result);

        return new SuccessDataResult<Order>(result);
    }

    public IDataResult<Order> AddItem(OrderItemDto orderItemDto)
    {
        var orderItem = _orderItemService.Create(orderItemDto);

        if (!orderItem.Success) return new ErrorDataResult<Order>(orderItem.Message);

        _discountService.ClearDiscounts(orderItemDto.OrderId);

        var order = _orderDal.GetWithDetails(orderItemDto.OrderId);

        Calculate(order);

        return new SuccessDataResult<Order>(order);
    }

    public IDataResult<GetOrderDto> GetOrder(long id)
    {
        var order = _orderDal.GetWithDetails(id);

        if (order == null) return new ErrorDataResult<GetOrderDto>("Order not found");

        Calculate(order);

        var getOrderDto = new GetOrderDto();

        return new SuccessDataResult<GetOrderDto>(getOrderDto);
    }

    private void Calculate(Order order)
    {
        CalculateTotal(order);

        ApplyDiscounts(order);

        _entityRepository.Update(order);
    }

    private void CalculateTotal(Order order)
    {
        order.Total = order.Items.Sum(x => x.ProductPrice);
    }

    private void ApplyDiscounts(Order order)
    {
        foreach (var ruleService in _discountRuleServices)
        {
            var result = ruleService.GetDiscount(order);

            if (!result.Success) continue;

            order.Total -= result.Data.Amount;
            order.TotalDiscount += result.Data.Amount;
            result.Data.OrderId = order.Id;

            _discountService.Create(result.Data);
        }
    }
}