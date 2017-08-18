namespace BMX.Domain.Entities
{
    using System;

    /// <summary>
    /// A shortcut of <see cref="FullAuditedEntity"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public class FullAuditedEntity : IEntity<int>
    {
        public int Id { get; set; }

        public virtual long? CreatorUserId { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual long? LastModifierUserId { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual long? DeleterUserId { get; set; }

        public virtual DateTime? DeletionTime { get; set; }
    }
}
