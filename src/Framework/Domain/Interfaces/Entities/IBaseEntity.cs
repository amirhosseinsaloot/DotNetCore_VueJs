namespace Domain.Interfaces.Entities;

/// <summary>
/// Provides properties that need implement in entities. 
/// </summary>
public interface IBaseEntity : IEntity
{
    public int Id { get; set; }
}
