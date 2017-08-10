namespace DapperDemo
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class Machine
    {
        public bool Active { get; set; }

        public string Code { get; set; }

        public string Desc { get; set; }

        public int? FinalMachineId { get; set; }

        public int? FinalTenantId { get; set; }

        public Guid? ImageId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public int SortSeq { get; set; }

        public int State { get; set; }

        public int? TenantId { get; set; }

        public string UId { get; set; }
    }

    class Program
    {
        protected static string GetStateNameByCode(string code)
        {
            switch (code)
            {
                case "1": return "报警";
                case "2": return "运行";
                case "3": return "空闲";
                case "4": return "离线";
                default: throw new NotImplementedException();
            }
        }

        static void Main(string[] args)
        {
            var dt = new DataTable("State");
            dt.Columns.Add("TenantId", typeof(int));
            dt.Columns.Add("MachineId", typeof(long));
            dt.Columns.Add("MachineCode", typeof(string));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("StartTime", typeof(DateTime));
            dt.Columns.Add("EndTime", typeof(DateTime));
            dt.Columns.Add("Duration", typeof(decimal));
            dt.Columns.Add("Memo", typeof(string));

            var startTime = new DateTime(2016, 12, 8, 0, 0, 0);
            var endTime = new DateTime(2016, 12, 8, 9, 05, 24); // 2016-12-08 09:08:23.000
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            var code = ran.Next(1, 4);
            for (var i = startTime; i < endTime; i = i.AddMinutes(code * 10))
            {
                var row = dt.NewRow();
                row["TenantId"] = 5;
                row["MachineId"] = 113;
                row["MachineCode"] = "a29ed053a2f04fc1a0835cd9e0e3e873";
                row["Code"] = ran.Next(1, 4);
                row["Name"] = GetStateNameByCode(row["Code"].ToString());
                row["StartTime"] = i;
                row["EndTime"] = i.AddMinutes(code * 10);
                row["Duration"] = 60 * code * 10;
                row["Memo"] = string.Empty;
                dt.Rows.Add(row);
            }

            // 7.把数据搬运到SQL Server中
            List<SqlBulkCopyColumnMapping> ColumnMappings = new List<SqlBulkCopyColumnMapping>();
            SqlBulkCopyColumnMapping TenantId = new SqlBulkCopyColumnMapping("TenantId", "TenantId");
            ColumnMappings.Add(TenantId);
            SqlBulkCopyColumnMapping MachineId = new SqlBulkCopyColumnMapping("MachineId", "MachineId");
            ColumnMappings.Add(MachineId);
            SqlBulkCopyColumnMapping MachineCode = new SqlBulkCopyColumnMapping("MachineCode", "MachineCode");
            ColumnMappings.Add(MachineCode);
            SqlBulkCopyColumnMapping Code = new SqlBulkCopyColumnMapping("Code", "Code");
            ColumnMappings.Add(Code);
            SqlBulkCopyColumnMapping Name = new SqlBulkCopyColumnMapping("Name", "Name");
            ColumnMappings.Add(Name);
            SqlBulkCopyColumnMapping StartTime = new SqlBulkCopyColumnMapping("StartTime", "StartTime");
            ColumnMappings.Add(StartTime);
            SqlBulkCopyColumnMapping EndTime = new SqlBulkCopyColumnMapping("EndTime", "EndTime");
            ColumnMappings.Add(EndTime);
            SqlBulkCopyColumnMapping Duration = new SqlBulkCopyColumnMapping("Duration", "Duration");
            ColumnMappings.Add(Duration);
            SqlBulkCopyColumnMapping Memo = new SqlBulkCopyColumnMapping("Memo", "Memo");
            ColumnMappings.Add(Memo);

            using (SqlConnection conn = new SqlConnection(
                @"Server=114.55.172.95; Database=WIMICloud;User Id=sa; Password=aRRUf2W9XwUeTiVkIPFsGmu3IuPULKgE;pooling=true;connection lifetime=0;min pool size = 1;max pool size=512")
            )
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, transaction))
                    {
                        bulkCopy.DestinationTableName = "State";
                        foreach (var item in ColumnMappings)
                        {
                            bulkCopy.ColumnMappings.Add(item);
                        }

                        try
                        {
                            bulkCopy.WriteToServer(dt);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
        }
    }
}