using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Users;
using Core.Business.Implementations;
using Core.Utilities.Results;
using Data.Interfaces;
using Models.Dtos;
using Models.Entities;
using Models.Enums;

namespace Business.Implementations.Users;

public class UserManager : EntityManager<Models.Entities.User>, IUserService
{
    private IMapper _mapper;
    
    public UserManager(IUserDal userDal, IMapper mapper) : base(userDal)
    {
        _mapper = mapper;
    }

    public IDataResult<List<User>> List()
    {
        return new SuccessDataResult<List<User>>(_entityRepository.List());
    }

    public IDataResult<User> Create(UserDto dto)
    {
        var user = _entityRepository.Create(_mapper.Map<Models.Entities.User>(dto));
        return new SuccessDataResult<User>(user);
    }

    public IDataResult<User> GetByRoleFirst(RoleEnum role)
    {
        var user = _entityRepository.Get(x => x.Role == role);
        if (user == null) return new ErrorDataResult<User>();
        return new SuccessDataResult<User>(user);
    }
}