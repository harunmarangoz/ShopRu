namespace Core.Entities.Interfaces;

public interface ISoftDeleteEntity : IEntity
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDateTime { get; set; }
}