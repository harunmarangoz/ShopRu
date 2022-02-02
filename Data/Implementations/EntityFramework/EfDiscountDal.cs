using Core.DataAccess.Implementations.EntityFramework;
using Data.Implementations.EntityFramework.Contexts;
using Data.Interfaces;
using Models.Entities;

namespace Data.Implementations.EntityFramework;

public class EfDiscountDal : EfEntityRepositoryBase<Discount, DatabaseContext>, IDiscountDal
{
    public EfDiscountDal(DatabaseContext dbContext) : base(dbContext)
    {
    }
}