using System.Linq.Expressions;
using Core.Entities.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.DataAccess.Interfaces;

public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
{
    List<TEntity> List(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    TEntity? Get(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    TEntity Create(TEntity entity);
    TEntity? Update(TEntity entity);
    void Delete(TEntity entity);
}