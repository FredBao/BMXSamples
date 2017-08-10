namespace CURD
{
    using System.Linq;

    using MongoDB.Bson;
    using MongoDB.Driver;

    /// <summary>
    /// 基础的CURD操作，主要用于Mongo子节点的CURD
    /// </summary>
    class Program
    {
        private static string connectionString = System.Configuration.ConfigurationManager
            .AppSettings["ConnectionString"].ToString();

        private static string databaseName = System.Configuration.ConfigurationManager.AppSettings["DatabaseName"]
            .ToString();

        private static string macConfigCollection = System.Configuration.ConfigurationManager
            .AppSettings["MacConfigCollection"].ToString();

        private static string macInfoCollection = System.Configuration.ConfigurationManager
            .AppSettings["MacInfoCollection"].ToString();

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        private static IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            return db.GetCollection<BsonDocument>(collectionName);
        }

        static void Main(string[] args)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);

            // var collection = GetCollection(macInfoCollection);

            // 1.直接新增记录行
            // var document = new BsonDocument
            // {
            // { "name", "MongoDB" },
            // { "scores",new BsonArray { } }
            // };
            // collection.InsertOne(document);

            // 2在原有的节点基础上新增子节点s
            // var document = new BsonDocument
            // {
            // { "Count",1 },
            // { "Yield",1},
            // { "OrginalCount",1},
            // { "CreationTime",1 }
            // };

            // var filter = Builders<BsonDocument>.Filter.Eq("name", "joe");

            // var update = Builders<BsonDocument>.Update.Push<BsonDocument>("scores", document);

            // collection.FindOneAndUpdate(filter, update);

            // 3直接覆盖某个节点
            // var collection = GetCollection(macInfoCollection);
            // var filter = Builders<BsonDocument>.Filter.Eq("MacNo", machine.MacNo);
            // var count = collection.Find(filter).Count();
            // if (count == 0)
            // {
            // var document = new BsonDocument
            // {
            // { "MacNo", machine.MacNo },
            // { "ProcNo","" },
            // { "Map", mapArray
            // },
            // { "Alarm", new BsonArray{}
            // },
            // { "Status", new BsonArray{}
            // },
            // { "Capacity", new BsonArray{}
            // },
            // { "Parameter"+DateTime.Now.ToString("yyyyMM"), new BsonArray{}
            // },
            // { "CreationTime",DateTime.Now.ToString("yyyyMMddHHmmss") }
            // };
            // collection.InsertOne(document);
            // }
            // else
            // {
            // FieldDefinition<BsonDocument, BsonArray> targetField = "Map";

            // var update = Builders<BsonDocument>.Update.Set(targetField, mapArray);

            // collection.FindOneAndUpdate(filter, update);
            // }
            #region 更新

            // 更新root节点
            // var document = new BsonDocument
            // {
            // { "name","dsadas" },
            // };
            // var filter = Builders<BsonDocument>.Filter.Eq("name", document);

            // var update = Builders<BsonDocument>.Update.Set("name", "Hello");

            // collection.FindOneAndUpdate(filter, update);

            // 更新子节点
            // var array = new BsonArray()
            // {
            // new BsonDocument
            // {
            // { "Count",1 },
            // { "Yield",1},
            // { "OrginalCount",1},
            // { "CreationTime",1 }
            // }
            // };

            // FieldDefinition<BsonDocument, BsonDocument> CapacityField = "scores";

            // var targetDocument = new BsonDocument
            // {
            // { "Count",1 },
            // { "Yield",1},
            // { "OrginalCount",1},
            // { "CreationTime",1 }
            // };

            // var sourceDocument = new BsonDocument
            // {
            // { "Count",33333 },
            // { "Yield",23333333},
            // { "OrginalCount",33333},
            // { "CreationTime",33333 }
            // };

            // var filter = Builders<BsonDocument>.Filter.Eq(CapacityField, targetDocument);

            // var count = collection.Find(filter).Count();

            // if (count == 0)
            // {
            // var update = Builders<BsonDocument>.Update.Push(CapacityField, sourceDocument);

            // collection.FindOneAndUpdate(filter, update);
            // }
            // else
            // {
            // var update = Builders<BsonDocument>.Update.Set(CapacityField, sourceDocument);

            // collection.FindOneAndUpdate(filter, update);
            // }

            // 3.更新多个文档根据某个条件
            // var collection = GetCollection("Alarm");
            // var document = new BsonDocument
            // {
            // { "AlarmEndTime",DateTime.Now.ToString("yyyyMMddHHmmss") },
            // };
            // var filter =
            // Builders<BsonDocument>.Filter.Eq("HostCode", "WIMI")
            // &
            // Builders<BsonDocument>.Filter.Eq("MachineCode", "2000000000000000000000000000000000000000000000000000000000000000")
            // &
            // Builders<BsonDocument>.Filter.Eq("AlarmEndTime", "");

            // var update = Builders<BsonDocument>.Update.Set("AlarmEndTime", DateTime.Now.ToString("yyyyMMddHHmmss"));

            // collection.UpdateMany(filter, update);
            #endregion

            #region 查询

            // 1查询一个集合是否存在数据库里
            // var filter = new BsonDocument("name", "Exceptions");
            ////filter by collection name
            // var collections = db.ListCollections(new ListCollectionsOptions { Filter = filter });
            ////check for existence
            // var count = collections.ToList().Count;
            // if (count == 0)
            // { }

            // var collection2 = GetCollection("Exceptions").Database.;

            // var exceptionCollection = "Exceptions";
            ////1查询集合是否存在数据库里
            // var filter = new BsonDocument("name", exceptionCollection);
            ////filter by collection name
            // var collections = db.ListCollections(new ListCollectionsOptions { Filter = filter });
            ////check for existence
            // var count = collections.ToList().Count;
            // string exceptionTableName = "Exception" + DateTime.Now.ToString("yyyyMMdd");
            // if (count == 0)
            // {
            // db.CreateCollection(exceptionCollection);
            // var collection = db.GetCollection<BsonDocument>(exceptionCollection);
            // //创建集合并把错误信息插入到集合中
            // var document = new BsonDocument {
            // { "_id",exceptionTableName },
            // { "ExceptionArray",new BsonArray { } }
            // };
            // collection.InsertOne(document);
            // }
            // else
            // {
            // //把错误信息插入到集合中

            // var collection = GetCollection(exceptionCollection);
            // var result = collection.Find(new BsonDocument()).ToList();
            // if (!result[0].ToString().Contains(exceptionTableName))
            // {
            // var document = new BsonDocument
            // {
            // { "_id",exceptionTableName },
            // { "ExceptionArray",new BsonArray { } }
            // };
            // collection.InsertOne(document);
            // }

            // var document2 = new BsonDocument()
            // {
            // { "MacNo", "TestMacNo" },
            // { "Message","adasdsda" }
            // };
            // //document2.Add(new BsonElement("Hello", "ExceptionMessage"));
            // //document2.Add(new BsonElement("CreationTime", DateTime.Now.ToString("yyyyMMddHHmmss")));
            // FieldDefinition<BsonDocument> field = "ExceptionArray";
            // var filter2 = Builders<BsonDocument>.Filter.Eq("_id", exceptionTableName);
            // var update = Builders<BsonDocument>.Update.Push(field, document2);
            // collection.FindOneAndUpdate<BsonDocument>(filter2, update);
            // }

            // 2.获取一个字段最大值记录
            // var collection = GetCollection("Parameter201607");
            // var filter = new BsonDocument("MachineCode", "4111111111111111111111111111111111111111111111111111111111111111");
            // var sort = Builders<BsonDocument>.Sort.Descending("CreationTime");
            // var CursorToResults = collection.Find<BsonDocument>(filter).Sort(sort).Limit(1);
            // var RecordwithMax_dt_Value = CursorToResults.FirstOrDefault();
            // string HostCode = ConfigurationManager.AppSettings["HostCode"].ToString();
            ////3.IN 操作
            // var collection = GetCollection("MachineInfo");
            ////var filter = Builders<BsonDocument>.Filter.In("MachineCode", new List<string>() { "2111111111111111111111111111111111111111111111111111111111111111", "3111111111111111111111111111111111111111111111111111111111111111" });
            // var filter = Builders<BsonDocument>.Filter.Eq("IsActive", true);
            // var sort = Builders<BsonDocument>.Sort.Descending("CreationTime");
            // var CursorToResults = collection.Find<BsonDocument>(filter).ToList();

            // 4.查询并且更新一个子集合中的字段
            // var collection = GetCollection("MachineInfo");
            // var filter =
            // Builders<BsonDocument>.Filter.Eq("TenantId", 5)
            // &
            // Builders<BsonDocument>.Filter.Eq("MachineCode", "3100000000000000000000000000000000000000000000000000000000000000");
            // var count = collection.Find(filter).FirstOrDefault()["Map"].AsBsonArray.Where(s => Convert.ToInt32(s["Id"]) > 20).ToList();
            // var document = new BsonDocument();
            // for (int i = 0; i < count.Count; i++)
            // {
            // document.Add(new BsonElement("" + count[i]["Value"] + "", ""));
            // }
            // FieldDefinition<BsonDocument> targetField = "Parameter.spn_speed";
            // var update = Builders<BsonDocument>.Update.Set("Capacity.CurrentProgramCount", 0);
            // FieldDefinition<BsonDocument, BsonDocument> targetField = "Parameter";
            // var update = Builders<BsonDocument>.Update.Set(targetField, document);
            // collection.FindOneAndUpdate(filter, update);

            // 5.查询集合最新10笔数据

            // 查询过滤条件
            var collection = GetCollection("Parameter201611");
            SortDefinition<BsonDocument> sort = new BsonDocument("CreationTime", -1);
            var filter = Builders<BsonDocument>.Filter.Eq("MachineId", 3);
            var projection = Builders<BsonDocument>.Projection.Exclude("_id").Exclude("MachineId")
                .Exclude("CreationTime");

            var result = collection.Find(filter).Project(projection).Sort(sort).Limit(10).ToList();
            foreach (var item in result)
            {
                var tempList = item.Elements.ToList();
                for (int i = 0; i < tempList.Count(); i++)
                {
                    var name = tempList[i].Name;
                    var value = tempList[i].Value;
                }
            }

            #endregion

            #region Test

            // var filter =
            // Builders<BsonDocument>.Filter.Gt("Alarm.CreationTime", dataSpans[i].StartTime)
            // &
            // Builders<BsonDocument>.Filter.Lte("Alarm.CreationTime", dataSpans[i].EndTime);
            // var projection = Builders<BsonDocument>.Projection.Exclude("_id").Include("Alarm").Include("MacNo");

            // var filter = Builders<BsonDocument>.Filter.Gte("Alarm.CreationTime", 20160720202032);
            // var projection = Builders<BsonDocument>.Projection.Exclude("_id").Include("Alarm").Include("MacNo");
            // var result = collection.Find<BsonDocument>(filter).Project(projection).ToList()[0][1].AsBsonValue.AsBsonArray[0]["Code"];//.Where(e=>e["CreationTime"] >20160720202044).ToList();

            // long startTime = 20160721094259;  //DateTime.Now.AddMinutes(-1).ToString("yyyyMMddHHmmss"); 
            // var parameterKey = "Parameter" + DateTime.Now.ToString("yyyyMM");
            // var doFilter = Builders<BsonDocument>.Filter.Eq("MacNo", "2111111111111111111111111111111111111111111111111111111111111111") & Builders<BsonDocument>.Filter.Gte(parameterKey + ".CreationTime", startTime);

            // var projection = Builders<BsonDocument>.Projection
            // .Include("MacNo").Include(parameterKey)
            // .Exclude("_id");//.Slice(parameterKey, -1);

            // var parameterDocument = collection.Find(doFilter).Project(projection).ToList();
            #endregion
        }
    }
}