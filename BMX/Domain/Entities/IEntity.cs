namespace BMX.Domain.Entities
{
    /// <summary>
    ///  Defines interface for base entity type. All entities in the system must implement
    ///  this interface.
    /// </summary>
    public interface IEntity : IEntity<int>
    {
    }
}