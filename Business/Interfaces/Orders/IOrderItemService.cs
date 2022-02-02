using Core.Business.Interfaces;
using Core.Utilities.Results;
using Models.Dtos;
using Models.Entities;

namespace Business.Interfaces.Orders;

public interface IOrderItemService : IEntityService<OrderItem>
{
    IDataResult<OrderItem> Create(OrderItemDto orderItem);
}