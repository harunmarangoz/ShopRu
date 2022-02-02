using System.Linq.Expressions;
using Core.Entities.Interfaces;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Business.Interfaces;

public interface IEntityService<T> where T : class, IEntity, new()
{
}