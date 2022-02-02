using System.Data.Entity;
using Core.DataAccess.Implementations.EntityFramework;
using Data.Implementations.EntityFramework.Contexts;
using Data.Interfaces;
using Models.Entities;

namespace Data.Implementations.EntityFramework;

public class EfOrderDal : EfEntityRepositoryBase<Order, DatabaseContext>, IOrderDal
{
    public EfOrderDal(DatabaseContext dbContext) : base(dbContext)
    {
    }

    public Order GetWithDetails(long id)
    {
        return _dbContext.Orders.Include(x => x.Items).Include(x => x.Discounts).FirstOrDefault(x => x.Id == id);
    }
}