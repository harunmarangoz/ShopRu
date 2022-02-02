using Core.DataAccess.Interfaces;
using Models.Entities;

namespace Data.Interfaces;

public interface IOrderItemDal : IEntityRepository<OrderItem>
{
}