using Core.Entities.Interfaces;

namespace Core.Entities.Implementations;

public class SoftDeleteEntity : Entity, ISoftDeleteEntity
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDateTime { get; set; }
}