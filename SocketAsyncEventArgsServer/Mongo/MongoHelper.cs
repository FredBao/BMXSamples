namespace SocketAsyncEventArgsServer.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FlexibleAOPComponent;

    using MongoDB.Bson;
    using MongoDB.Driver;

    using SocketAsyncEventArgsServer.Helper;

    public class MongoHelper : ObjectWithAspects
    {
        private static string connectionString = System.Configuration.ConfigurationManager
            .AppSettings["ConnectionString"].ToString();

        private static string databaseName = System.Configuration.ConfigurationManager.AppSettings["DatabaseName"]
            .ToString();

        private static string exceptionCollection = System.Configuration.ConfigurationManager
            .AppSettings["ExceptionCollection"].ToString();

        private static string macConfigCollection = System.Configuration.ConfigurationManager
            .AppSettings["MacConfigCollection"].ToString();

        /// <summary>
        /// 获取登录用户名和密码
        /// </summary>
        public int GetMacLoginInfo(AsyncUserToken token)
        {
            try
            {
                var collection = this.GetCollection(macConfigCollection);
                var filter = Builders<BsonDocument>.Filter.Eq("MachineCode", token.Machine.MachineCode)
                             & Builders<BsonDocument>.Filter.Eq("IsActive", true);
                var result = collection.Find(filter).ToList();
                if (result.Count > 0)
                {
                    token.UserName = Convert.ToByte(result[0]["UId"]);
                    token.Password = result[0]["Password"].AsString;
                    token.TenantId = result[0]["TenantId"].AsString;
                    token.Machine.Uid = token.UserName;
                    token.Machine.Password = token.Password;
                    token.Machine.TenantId = token.TenantId;
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.InfoFormat("在查询设备信息的时候发送错误:{0}", ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 将获取到的参数映射表插入的Mongo数据库中
        /// </summary>
        /// <param name="machine"></param>
        public int InsertMappedFiled(Machine machine)
        {
            try
            {
                var mapArray = new BsonArray();

                foreach (var item in machine.Map.MapArray)
                {
                    mapArray.Add(new BsonDocument() { { "Id", item.Id.TrimEnd() }, { "Value", item.Value.TrimEnd() } });
                }

                var collection = this.GetCollection(macConfigCollection);
                var filter = Builders<BsonDocument>.Filter.Eq("TenantId", machine.TenantId)
                             & Builders<BsonDocument>.Filter.Eq("MachineCode", machine.MachineCode);
                var count = collection.Find(filter).Count();
                if (count == 0)
                {
                    return -1;
                }
                else
                {
                    FieldDefinition<BsonDocument, BsonArray> targetField = "Map";
                    var update = Builders<BsonDocument>.Update.Set(targetField, mapArray);
                    collection.FindOneAndUpdate(filter, update);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.InfoFormat("在接收枚举数据的时候发送错误:{0}", ex.Message);
                return -1;
            }
        }

        /// DT：1 字节整数，数据类别,最高位表示是否可写入[0=只读，1=读写]，其它位数值表示数据类别[0=字符串,1=双精度浮
        /// 点数,2=整数(32 位),3=字节,4=布尔值,5=字节块]
        /// <summary>
        /// 将采集到数据根据设备类型和字段Id插入到Mongo数据库中
        /// </summary>
        /// <param name="dataItems"></param>
        public int InsertValueToMongo(Machine machine, List<DataItem> dataItems)
        {
            try
            {
                var creationTime = DateTime.Now.ToString("yyyyMMddHHmmss");

                var alarmIds = new ushort[2] { (ushort)ConfigField.AlarmCode, (ushort)ConfigField.AlarmMsg };
                //是否同时存在报警编号和报警内容
                if (dataItems.Where(i => alarmIds.Contains(i.Id)).Count() > 1)
                {
                    //取出报警编号和报警内容
                    var alarmCode = SocketHelper
                        .GetValueByType(dataItems.Where(i => i.Id == ((ushort)ConfigField.AlarmCode)).FirstOrDefault())
                        .ToString();
                    var alarmMessage = SocketHelper
                        .GetValueByType(dataItems.Where(i => i.Id == ((ushort)ConfigField.AlarmMsg)).FirstOrDefault())
                        .ToString();
                    //插入到数据库
                    this.InsertAlarmData(
                        machine.TenantId,
                        machine.MachineCode,
                        new AlarmArray { Code = alarmCode, Message = alarmMessage, CreationTime = creationTime });
                    //剔除已取得的数据
                    dataItems = dataItems.Where(i => alarmIds.Contains(i.Id) == false).ToList();
                }
                //获得最新程序号
                string procNo = this.GetNewestProcNo(machine, dataItems);
                var parameters = new List<BsonElement>();

                for (int i = 0; i < dataItems.Count; i++)
                {
                    switch (dataItems[i].Id)
                    {
                        case 1:
                            //单报警号，需要取得上次的报警内容
                            var alarmCode = SocketHelper
                                .GetValueByType(
                                    dataItems.Where(item => item.Id == ((ushort)ConfigField.AlarmCode))
                                        .FirstOrDefault()).ToString(); // Encoding.Default.GetString(.Value);
                            var filter = Builders<BsonDocument>.Filter.Eq("TenantId", machine.TenantId)
                                         & Builders<BsonDocument>.Filter.Eq("MachineCode", machine.MachineCode);
                            var sort = Builders<BsonDocument>.Sort.Descending("CreationTime");
                            var collection = this.GetCollection("Alarm");
                            var cursorToResults = collection.Find<BsonDocument>(filter).Sort(sort);
                            var lastAlarmMessage = cursorToResults.FirstOrDefault() == null
                                                       ? ""
                                                       : cursorToResults.FirstOrDefault()["Message"].AsString;
                            //插入到数据库
                            this.InsertAlarmData(
                                machine.TenantId,
                                machine.MachineCode,
                                new AlarmArray
                                    {
                                        Code = alarmCode,
                                        Message = lastAlarmMessage,
                                        CreationTime = creationTime
                                    });
                            //更新状态机
                            machine.State.LastAlarmCode = alarmCode;
                            break;
                        case 2:
                            //单报警内容，报警为Unknown
                            var alarmMessage = SocketHelper
                                .GetValueByType(
                                    dataItems.Where(item => item.Id == ((ushort)ConfigField.AlarmMsg)).FirstOrDefault())
                                .ToString();
                            //插入到数据库
                            this.InsertAlarmData(
                                machine.TenantId,
                                machine.MachineCode,
                                new AlarmArray
                                    {
                                        Code = "Unknown",
                                        Message = alarmMessage,
                                        CreationTime = creationTime
                                    });
                            break;
                        case 3:
                            //设备状态
                            var statusCode = SocketHelper
                                .GetValueByType(
                                    dataItems.Where(item => item.Id == ((ushort)ConfigField.StateCode))
                                        .FirstOrDefault()).ToString();
                            //插入到数据库
                            this.InsertStateData(
                                machine.TenantId,
                                machine.MachineCode,
                                new StateArray { Code = statusCode, CreationTime = creationTime });
                            break;
                        case 4:
                            //采集到的计数器值
                            var count = Convert.ToInt32(SocketHelper.GetValueByType(dataItems[i]));
                            //取得产量累加值
                            var filter2 = Builders<BsonDocument>.Filter.Eq("TenantId", machine.TenantId)
                                          & Builders<BsonDocument>.Filter.Eq("MachineCode", machine.MachineCode);
                            var sort2 = Builders<BsonDocument>.Sort.Descending("CreationTime");
                            var collection2 = this.GetCollection("Capacity");
                            var cursorToResults2 = collection2.Find<BsonDocument>(filter2).Sort(sort2);
                            var totalCount = cursorToResults2.FirstOrDefault() == null
                                                 ? 0
                                                 : cursorToResults2.FirstOrDefault()["Count"].AsInt32;
                            var lastCount = cursorToResults2.FirstOrDefault() == null
                                                ? 0
                                                : cursorToResults2.FirstOrDefault()["OriginalCount"].AsInt32;
                            //判读是否需要插入产量，与上一次计数器数值比较，如果小于上一次则视为，计数器清零,记录产量为0
                            if (count > lastCount)
                            {
                                var yield = count - lastCount;
                                var originalCount = count;
                                totalCount += count - lastCount;
                                this.InsertCapacityData(
                                    machine.TenantId,
                                    machine.MachineCode,
                                    new CapacityArray
                                        {
                                            Count = totalCount,
                                            Yield = yield,
                                            OrginalCount = originalCount,
                                            ProgramName = procNo,
                                            CreationTime = creationTime
                                        });
                            }
                            else
                            {
                                var yield = 0;
                                var originalCount = 0;
                                totalCount += 0;
                                this.InsertCapacityData(
                                    machine.TenantId,
                                    machine.MachineCode,
                                    new CapacityArray
                                        {
                                            Count = totalCount,
                                            Yield = yield,
                                            OrginalCount = originalCount,
                                            ProgramName = procNo,
                                            CreationTime = creationTime
                                        });
                            }
                            //machine.State.LastCount = count;
                            break;
                        case 5: break;
                        case 6: break;
                        case 7: break;
                        case 8: break;
                        case 9: break;
                        case 10: break;
                        case 11: break;
                        case 12: break;
                        case 13: break;
                        case 14: break;
                        case 15: break;
                        case 16: break;
                        case 17: break;
                        case 18: break;
                        case 19: break;
                        case 20: break;
                        default:
                            //InsertParameterData(machine.MachineCode, GetNameById(machine.MachineCode, dataItems[i]), SocketHelper.GetValueByType(dataItems[i]), creationTime);
                            parameters.Add(
                                new BsonElement(
                                    this.GetNameById(machine.MachineCode, dataItems[i]) ?? string.Empty.ToString(),
                                    SocketHelper.GetValueByType(dataItems[i]).ToString()));
                            break;
                    }
                }
                //如果有接收到参数
                if (parameters.Count > 0)
                {
                    parameters.Add(new BsonElement("TenantId", machine.TenantId));
                    parameters.Add(new BsonElement("MachineCode", machine.MachineCode));
                    this.InsertParameterData(machine.MachineCode, parameters, creationTime);
                }
                return 1;
            }
            catch (Exception ex)
            {
                LogHelper.Log.InfoFormat("在接收采集数据的时候发送错误:{0}", ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 报警
        /// </summary>
        internal void InsertAlarmData(string tenantId, string machineCode, AlarmArray item)
        {
            var collection = this.GetCollection("Alarm");
            if (item.Code == "0")
            {
                //如果报警编号为0,则视为所有报警结束,更新所有的报警记录
                var document = new BsonDocument { { "AlarmEndTime", item.CreationTime }, };
                var filter = Builders<BsonDocument>.Filter.Eq("TenantId", tenantId)
                             & Builders<BsonDocument>.Filter.Eq("MachineCode", machineCode)
                             & Builders<BsonDocument>.Filter.Eq("AlarmEndTime", "");
                var update = Builders<BsonDocument>.Update.Set("AlarmEndTime", DateTime.Now.ToString("yyyyMMddHHmmss"));
                collection.UpdateMany(filter, update);
            }
            else
            {
                var document = new BsonDocument
                                   {
                                       { "TenantId", tenantId },
                                       { "MachineCode", machineCode },
                                       { "Code", item.Code ?? string.Empty },
                                       { "Message", item.Message ?? string.Empty },
                                       { "CreationTime", item.CreationTime ?? string.Empty },
                                       { "AlarmEndTime", string.Empty }
                                   };
                collection.InsertOne(document);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        internal void InsertStateData(string tenantId, string machineCode, StateArray item)
        {
            var collection = this.GetCollection("State");
            var document = new BsonDocument
                               {
                                   { "TenantId", tenantId },
                                   { "MachineCode", machineCode },
                                   { "Code", item.Code },
                                   { "CreationTime", item.CreationTime }
                               };
            collection.InsertOne(document);
        }

        /// <summary>
        /// 获取对应的集合
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        private IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            //1查询集合是否存在数据库里
            var filter = new BsonDocument("name", collectionName);
            //filter by collection name
            var collections = db.ListCollections(new ListCollectionsOptions { Filter = filter });
            //check for existence
            var count = collections.ToList().Count;
            if (count == 0)
            {
                db.CreateCollection(collectionName);
            }
            return db.GetCollection<BsonDocument>(collectionName);
        }

        /// <summary>
        /// 获取映射名称
        /// </summary>
        /// <param name="machineCode"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetNameById(string machineCode, DataItem item)
        {
            string returnResult = "";
            var collection = this.GetCollection(macConfigCollection);
            var filter = Builders<BsonDocument>.Filter.Eq("MachineCode", machineCode);
            var filter2 = Builders<BsonDocument>.Filter.Eq("Map.Id", item.Id.ToString());
            var result = collection.Find(filter & filter2).FirstOrDefault();
            if (result != null)
            {
                if (result["Map"].AsBsonArray.Where(s => s["Id"] == item.Id.ToString()).FirstOrDefault() != null)
                {
                    returnResult =
                        result["Map"].AsBsonArray.Where(s => s["Id"] == item.Id.ToString()).FirstOrDefault()["Value"]
                            .ToString();
                }
            }
            else
            {
                returnResult = "Unknown";
            }
            return returnResult.TrimEnd();
        }

        /// <summary>
        /// 获得最新程序号
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="dataItems"></param>
        /// <returns></returns>
        private string GetNewestProcNo(Machine machine, List<DataItem> dataItems)
        {
            string result = "";
            var filter = Builders<BsonDocument>.Filter.Eq("TenantId", machine.TenantId)
                         & Builders<BsonDocument>.Filter.Eq("MachineCode", machine.MachineCode);
            //是否有更新程序号
            if (dataItems.Where(i => i.Id == (ushort)ConfigField.ProgramName).Count() > 0)
            {
                //更新当前程序号
                result = SocketHelper.GetValueByType(
                    dataItems.Where(i => i.Id == (ushort)ConfigField.ProgramName).FirstOrDefault()).ToString();
                var collection = this.GetCollection(macConfigCollection);
                FieldDefinition<BsonDocument, string> targetField = "ProgramName";
                var update = Builders<BsonDocument>.Update.Set(targetField, result);
                collection.FindOneAndUpdate(filter, update);
            }
            else
            {
                var collection = this.GetCollection(macConfigCollection);
                var item = collection.Find(filter).FirstOrDefault();
                if (item != null)
                {
                    result = item["ProgramName"].ToString();
                }
            }
            return result ?? string.Empty;
        }

        /// <summary>
        /// 产量
        /// </summary>
        private void InsertCapacityData(string tenantId, string machineCode, CapacityArray item)
        {
            var collection = this.GetCollection("Capacity");
            var filter = new BsonDocument("MachineCode", machineCode);
            var sort = Builders<BsonDocument>.Sort.Descending("Count");
            var CursorToResults = collection.Find<BsonDocument>(filter).Sort(sort);
            var RecordwithMax_dt_Value = CursorToResults.FirstOrDefault() == null
                                             ? 0
                                             : CursorToResults.FirstOrDefault()["Count"].AsInt32;

            var document = new BsonDocument
                               {
                                   { "TenantId", tenantId },
                                   { "MachineCode", machineCode },
                                   { "Count", item.Count },
                                   { "Yield", item.Yield },
                                   { "OriginalCount", item.OrginalCount },
                                   { "ProgramName", item.ProgramName ?? string.Empty },
                                   { "CreationTime", item.CreationTime ?? string.Empty }
                               };
            collection.InsertOne(document);
        }

        /// <summary>
        /// 参数
        /// </summary>
        private void InsertParameterData(string machineCode, List<BsonElement> parameters, string creationTime)
        {
            var collectionName = "Parameter" + creationTime.Substring(0, 6);
            var collection = this.GetCollection(collectionName);
            parameters.Add(new BsonElement("CreationTime", creationTime ?? string.Empty));
            var document = new BsonDocument();
            for (int i = 0; i < parameters.Count; i++)
            {
                document.Add(parameters[i]);
            }
            collection.InsertOne(document);
        }
    }
}