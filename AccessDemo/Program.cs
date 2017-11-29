using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            GetQMParameter();
        }

        public static List<AuxiliaryParameter> GetQMParameter()
        {
            var returnValue = new List<AuxiliaryParameter>();

            using (OleDbConnection conn = GetQMConnection())
            {
                var executeSql = @"
SELECT 
时间 AS RUNNING_DATE,
腔体测试数据 AS P_FLOAT1,
腔体测试结果 AS P_FLOAT2,
高压油道数据1 AS P_FLOAT3,
高压油道结果1 AS P_FLOAT4,
高压油道数据2 AS P_FLOAT5,
高压油道结果2 AS P_FLOAT6,
高压油道数据3 AS P_FLOAT7,
高压油道结果3 AS P_FLOAT8,
悬置孔数据 AS P_FLOAT9,
悬置孔结果 AS P_FLOAT10
FROM 组合测试记录";

                conn.Open();
                returnValue = conn.Query<AuxiliaryParameter>(executeSql).ToList();
                conn.Close();
            }

            return returnValue;
        }

        private static OleDbConnection GetQMConnection()
        {
            return new OleDbConnection(@"Provider=Microsoft.Ace.OleDb.12.0;Data Source=\\192.168.0.98\气密机\database.mdb;Persist Security Info=False;");
        }
    }

    public class AuxiliaryParameter
    {
        public string RUNNING_DATE { get; set; }
        public string P_FLOAT1 { get; set; }
        public string P_FLOAT2 { get; set; }
        public string P_FLOAT3 { get; set; }
        public string P_FLOAT4 { get; set; }
        public string P_FLOAT5 { get; set; }
        public string P_FLOAT6 { get; set; }
        public string P_FLOAT7 { get; set; }
        public string P_FLOAT8 { get; set; }
        public string P_FLOAT9 { get; set; }
        public string P_FLOAT10 { get; set; }
        public string P_FLOAT11 { get; set; }
        public string P_FLOAT12 { get; set; }
        public string P_FLOAT13 { get; set; }
        public string P_FLOAT14 { get; set; }
        public string P_FLOAT15 { get; set; }
        public string P_FLOAT16 { get; set; }
        public string P_FLOAT17 { get; set; }
        public string P_FLOAT18 { get; set; }
        public string P_FLOAT19 { get; set; }
        public string P_FLOAT20 { get; set; }
        public string P_FLOAT21 { get; set; }
        public string P_FLOAT22 { get; set; }
        public string P_FLOAT23 { get; set; }
        public string P_FLOAT24 { get; set; }
        public string P_FLOAT25 { get; set; }
        public string P_FLOAT26 { get; set; }
        public string P_FLOAT27 { get; set; }
        public string P_FLOAT28 { get; set; }
        public string P_FLOAT29 { get; set; }
        public string P_FLOAT30 { get; set; }
    }
}
