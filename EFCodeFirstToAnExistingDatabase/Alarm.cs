namespace EFCodeFirstToAnExistingDatabase
{
    using Bmx.Abp.Domain.Entities;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class Alarm : MongoEntity
    {
        public string Code { get; set; }

        public string CreationTime { get; set; }

        public string MachineCode { get; set; }

        public int MachineId { get; set; }

        public int MachinesShiftDetailId { get; set; }

        public string Message { get; set; }

        public int OrderId { get; set; }

        public string PartNo { get; set; }

        public int ProcessId { get; set; }

        public string ProgramName { get; set; }

        public long UserId { get; set; }

        public int UserShiftDetailId { get; set; }

        public override string CollectionName { get; } = "Alarm";
    }
}
