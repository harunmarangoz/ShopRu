using Core.Business.Interfaces;
using Core.Utilities.Results;
using Models.Dtos;
using Models.Entities;

namespace Business.Interfaces.Orders;

public interface IOrderService : IEntityService<Order>
{
    IDataResult<List<Order>> List();
    IDataResult<Order> Create(CreateOrderDto order);
    IDataResult<GetOrderDto> GetOrder(long id);
    IDataResult<Order> AddItem(OrderItemDto orderItemDto);
}