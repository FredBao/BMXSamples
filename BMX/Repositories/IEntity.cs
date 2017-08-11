namespace BMX.Repositories
{
    /// <summary>
    ///  Defines interface for base entity type. All entities in the system must implement
    ///  this interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 摘要:Unique identifier for this entity.
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}