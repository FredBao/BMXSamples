namespace SocketAsyncEventArgsServer
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;

    using SocketAsyncEventArgsServer.Mongo;

    public class AsyncUserToken
    {
        // 最后一次接收数据包时间点
        public DateTime ActiveDateTime = DateTime.Now;

        // 命令包
        public BasicPackage BasicPackage;

        // 采集数据项的个数
        public int CollectDataCount = 0;

        // 设备制造厂商编号
        public string HostCode = string.Empty;

        // 已入库的数据
        public int InsertedCount = 0;

        // 是否登录成功
        public bool IsLogined = false;

        // 要读取的字节长度,初始化为8
        public int Length = 8;

        // 枚举数据项的个数
        public int MappedFieldCount = 0;

        // 登录机器的密码
        public string Password = string.Empty;

        // Client  socket.
        public Socket Socket = null;

        // 设备编号
        public string TenantId = string.Empty;

        // 登录机器的用户名(0~255)
        public byte UserName = 0xff;

        // 数据项编号
        internal ushort DID;

        // 数据项类型
        internal byte DT;

        // 是否支持数据推送功能
        internal bool IsSupportPushData = false;

        internal int Size = 0;

        // 数据项值
        internal byte[] Value;

        public AsyncUserToken()
        {
            this.Machine = new Machine();
            this.DataItems = new List<DataItem>();
            this.BasicPackage = new BasicPackage();
            this.resultBytes = new List<byte>();
            this.CommandType = CommandType.PackagePrefix;
            this.MongoHelper = new MongoHelper();
        }

        public CommandType CommandType { get; set; }

        // 接收的数据项集合
        public List<DataItem> DataItems { get; set; }

        // 设备对象
        public Machine Machine { get; set; }

        public MongoHelper MongoHelper { get; set; }

        // 暂存数据
        internal List<byte> resultBytes { get; set; }
    }
}