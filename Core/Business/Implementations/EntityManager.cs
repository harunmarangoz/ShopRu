using Core.Business.Interfaces;
using Core.DataAccess.Interfaces;
using Core.Entities.Interfaces;

namespace Core.Business.Implementations;

public class EntityManager<TEntity> : IEntityService<TEntity>
    where TEntity : class, IEntity, new()
{
    protected IEntityRepository<TEntity> _entityRepository;

    public EntityManager(IEntityRepository<TEntity> entityRepository)
    {
        _entityRepository = entityRepository;
    }
}