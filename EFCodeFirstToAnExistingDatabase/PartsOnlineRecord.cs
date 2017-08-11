namespace EFCodeFirstToAnExistingDatabase
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BMX.Repositories;

    [Table("PartsOnlineRecords")]
    public class PartsOnlineRecord : IEntity<int>
    {
        public int Id { get; set; }
        public string PartNo { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool IsComplete { get; set; }
    }
}
