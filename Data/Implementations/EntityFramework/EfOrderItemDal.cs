using Core.DataAccess.Implementations.EntityFramework;
using Data.Implementations.EntityFramework.Contexts;
using Data.Interfaces;
using Models.Entities;

namespace Data.Implementations.EntityFramework;

public class EfOrderItemDal : EfEntityRepositoryBase<OrderItem, DatabaseContext>, IOrderItemDal
{
    public EfOrderItemDal(DatabaseContext dbContext) : base(dbContext)
    {
    }
}