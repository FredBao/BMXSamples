namespace BMX.Domain.Entities
{
    using System;

    /// <summary>
    /// Implements <see>
    ///         <cref>IFullAudited</cref>
    ///     </see>
    ///     to be a base class for full-audited entities.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class FullAuditedEntity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }

        public virtual long? CreatorUserId { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual long? LastModifierUserId { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }
        
        public virtual bool IsDeleted { get; set; }
        
        public virtual long? DeleterUserId { get; set; }
        
        public virtual DateTime? DeletionTime { get; set; }
    }
}
