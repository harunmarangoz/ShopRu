using Core.DataAccess.Interfaces;
using Models.Entities;

namespace Data.Interfaces;

public interface IUserDal : IEntityRepository<User>
{
}