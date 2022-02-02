using Core.DataAccess.Implementations.EntityFramework;
using Data.Implementations.EntityFramework.Contexts;
using Data.Interfaces;
using Models.Entities;

namespace Data.Implementations.EntityFramework;

public class EfUserDal : EfEntityRepositoryBase<User, DatabaseContext>, IUserDal
{
    public EfUserDal(DatabaseContext dbContext) : base(dbContext)
    {
    }
}