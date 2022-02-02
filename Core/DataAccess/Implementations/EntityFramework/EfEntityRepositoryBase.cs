using System.Linq.Expressions;
using Core.DataAccess.Interfaces;
using Core.Entities.Implementations;
using Core.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.DataAccess.Implementations.EntityFramework;

public abstract class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
{
    protected readonly TContext _dbContext;

    protected EfEntityRepositoryBase(TContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<TEntity> List(Expression<Func<TEntity, bool>>? predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);
        if (include != null)
            query = include(query);

        return query.ToList();
    }

    public TEntity? Get(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query = _dbContext.Set<TEntity>().Where(predicate);

        if (include != null)
            query = include(query);

        return query.FirstOrDefault();
    }

    public TEntity Create(TEntity entity)
    {
        var addedEntity = _dbContext.Set<TEntity>().Add(entity);
        _dbContext.SaveChanges();
        return addedEntity.Entity;
    }

    public TEntity? Update(TEntity entity)
    {
        var updateEntity = Get(x => x.Id == entity.Id);
        if (updateEntity == null) return null;
        _dbContext.Entry(updateEntity).CurrentValues.SetValues(entity);
        _dbContext.Entry(updateEntity).State = EntityState.Modified;
        _dbContext.SaveChanges();
        return updateEntity;
    }

    public void Delete(TEntity entity)
    {
        if (entity is SoftDeleteEntity softDeleteEntity)
        {
            softDeleteEntity.DeletedDateTime = DateTime.Now;
            softDeleteEntity.IsDeleted = true;
            var entry = _dbContext.Entry(softDeleteEntity);
            entry.State = EntityState.Modified;
            _dbContext.SaveChanges();
            return;
        }

        var deleteEntity = _dbContext.Entry(entity);
        deleteEntity.State = EntityState.Deleted;
        _dbContext.SaveChanges();
    }
}