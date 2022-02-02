using Core.Entities.Interfaces;

namespace Core.Entities.Implementations;

public class Entity : IEntity
{
    public long Id { get; set; }
}