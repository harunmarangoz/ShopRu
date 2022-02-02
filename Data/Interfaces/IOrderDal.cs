using Core.DataAccess.Interfaces;
using Models.Entities;

namespace Data.Interfaces;

public interface IOrderDal : IEntityRepository<Order>
{
    Order GetWithDetails(long id);
}