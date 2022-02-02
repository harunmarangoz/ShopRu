using Core.Business.Interfaces;
using Core.Utilities.Results;
using Models.Dtos;
using Models.Entities;
using Models.Enums;

namespace Business.Interfaces.Users;

public interface IUserService : IEntityService<User>
{
    IDataResult<List<User>> List();
    IDataResult<User> Create(UserDto user);
    IDataResult<User> GetByRoleFirst(RoleEnum role);
}