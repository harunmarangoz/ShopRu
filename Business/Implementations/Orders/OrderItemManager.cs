using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Orders;
using Core.Business.Implementations;
using Core.Utilities.Results;
using Data.Interfaces;
using Models.Dtos;
using Models.Entities;

namespace Business.Implementations.Orders;

public class OrderItemManager : EntityManager<OrderItem>, IOrderItemService
{
    private IMapper _mapper;

    public OrderItemManager(IOrderItemDal orderItemDal, IMapper mapper) : base(orderItemDal)
    {
        _mapper = mapper;
    }

    public IDataResult<OrderItem> Create(OrderItemDto dto)
    {
        var orderItem = _entityRepository.Create(_mapper.Map<OrderItem>(dto));
        return new SuccessDataResult<OrderItem>(orderItem);
    }
}