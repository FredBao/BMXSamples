namespace SocketAsyncEventArgsServer
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using CGXBase.CGLogger;

    using FlexibleAOPComponent;

    using SocketAsyncEventArgs.Server;

    using SocketAsyncEventArgsServer.Helper;

    public class Server : ObjectWithAspects
    {
        // read, write (don't alloc buffer space for accepts)
        const int opsToPreAlloc = 2;

        // 会话超时时间
        private static int SocketTimeOutMS = 60;

        // the Socket used to listen for incoming connection requests 
        Socket listenSocket;

        // represents a large reusable set of buffers for all Socket operations
        BufferManager m_bufferManager;

        // 用于限制最大同时连接数量
        Semaphore m_maxNumberAcceptedClients;

        // the total number of clients connected to the server 
        int m_numConnectedSockets;

        // the maximum number of connections the sample is designed to handle simultaneously 
        private int m_numConnections;

        // pool of reusable SocketAsyncEventArgs objects for write, read and accept Socket operations
        SocketAsyncEventArgsPool m_readWritePool;

        // buffer size to use for each Socket I/O operation 
        private int m_receiveBufferSize;

        private Thread m_thread;

        // counter of the total # bytes received by the server
        int m_totalBytesRead;

        private List<SocketAsyncEventArgs> m_UsedObjects = new List<SocketAsyncEventArgs>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numConnections"></param>
        /// <param name="receiveBufferSize"></param>
        public Server(int numConnections, int receiveBufferSize)
        {
            this.m_totalBytesRead = 0;
            this.m_numConnectedSockets = 0;
            this.m_numConnections = numConnections;
            this.m_receiveBufferSize = receiveBufferSize;
            this.m_bufferManager = new BufferManager(
                receiveBufferSize * numConnections * opsToPreAlloc,
                receiveBufferSize);
            this.m_readWritePool = new SocketAsyncEventArgsPool(numConnections);
            this.m_maxNumberAcceptedClients = new Semaphore(numConnections, numConnections);
        }

        /// <summary>
        /// 初始化服务对象
        /// </summary>
        public void Init()
        {
            LogHelper.Log.InfoFormat("开启守护线程检测会话超时...");
            this.m_thread = new Thread(this.DaemonThreadStart);
            this.m_thread.Start();
            LogHelper.Log.InfoFormat("初始化可复用Buffer块...");
            this.m_bufferManager.InitBuffer();
            SocketAsyncEventArgs readWriteEventArg;
            LogHelper.Log.InfoFormat("初始化SocketAsyncEventArgs Pool...");
            for (int i = 0; i < this.m_numConnections; i++)
            {
                readWriteEventArg = new SocketAsyncEventArgs();
                readWriteEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(this.IO_Completed);
                readWriteEventArg.UserToken = new AsyncUserToken();
                this.m_bufferManager.SetBuffer(readWriteEventArg);
                this.m_readWritePool.Push(readWriteEventArg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localEndPoint"></param>
        public void Start(IPEndPoint localEndPoint)
        {
            this.listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.listenSocket.Bind(localEndPoint);
            this.listenSocket.Listen(100);
            this.StartAccept(null);

            // Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptEventArg"></param>
        public void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            LogHelper.Log.InfoFormat("开始监听...");
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(this.AcceptEventArg_Completed);
            }
            else
            {
                acceptEventArg.AcceptSocket = null;
            }

            // 信号量,用于控制最大并发连接客户端
            this.m_maxNumberAcceptedClients.WaitOne();

            bool willRaiseEvent = this.listenSocket.AcceptAsync(acceptEventArg);
            if (!willRaiseEvent)
            {
                this.ProcessAccept(acceptEventArg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            this.ProcessAccept(e);
        }

        /// <summary>
        /// 关闭守护线程
        /// </summary>
        private void Close()
        {
            this.m_thread.Abort();
            this.m_thread.Join();
        }

        /// <summary>
        /// 关闭连接，释放资源
        /// </summary>
        /// <param name="e"></param>
        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = e.UserToken as AsyncUserToken;
            LogHelper.Log.InfoFormat("客户端{0}:In CloseClientSocket", token.Socket.RemoteEndPoint.ToString());
            try
            {
                token.Socket.Shutdown(SocketShutdown.Send);
            }
            catch (Exception)
            {
            }

            token.Socket.Close();
            token = null;
            e.UserToken = null;
            e.UserToken = new AsyncUserToken();
            Interlocked.Decrement(ref this.m_numConnectedSockets);
            this.m_maxNumberAcceptedClients.Release();
            this.m_readWritePool.Push(e);
            this.m_UsedObjects.Remove(e);
        }

        /// <summary>
        /// 守护线程检测会话超时
        /// </summary>
        private void DaemonThreadStart()
        {
            while (this.m_thread.IsAlive)
            {
                LogHelper.Log.InfoFormat("当前连接数量{0}", this.m_UsedObjects.Count);
                for (int i = 0; i < this.m_UsedObjects.Count; i++)
                {
                    if (!this.m_thread.IsAlive)
                    {
                        break;
                    }

                    try
                    {
                        lock (this.m_UsedObjects[i].UserToken)
                        {
                            AsyncUserToken token = this.m_UsedObjects[i].UserToken as AsyncUserToken;
                            if (token != null)
                            {
                                var currentDateTime = DateTime.Now;
                                if ((currentDateTime - token.ActiveDateTime).TotalSeconds
                                    > SocketTimeOutMS)
                                {
                                    // 超时Socket断开
                                    LogHelper.Log.InfoFormat(
                                        "连接超时,当前连接设备为{0},当前时间为{1},最后通讯时间为{2}",
                                        token.TenantId,
                                        currentDateTime,
                                        token.ActiveDateTime);
                                    if (token.IsLogined)
                                    {
                                        // 设置此时设备是离线状态
                                        StateArray item =
                                            new StateArray()
                                                {
                                                    Code = "4",
                                                    CreationTime = DateTime.Now.ToString("yyyyMMddHHmmss")
                                                };
                                        token.MongoHelper.InsertStateData(
                                            token.Machine.TenantId,
                                            token.Machine.MachineCode,
                                            item);

                                        // 结束报警时间
                                        AlarmArray item2 =
                                            new AlarmArray()
                                                {
                                                    Code = "0",
                                                    CreationTime = DateTime.Now.ToString("yyyyMMddHHmmss")
                                                };
                                        token.MongoHelper.InsertAlarmData(
                                            token.Machine.TenantId,
                                            token.Machine.MachineCode,
                                            item2);
                                    }

                                    this.CloseClientSocket(this.m_UsedObjects[i]);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        CGLogger.Error(e.Message.ToString());
                    }
                }

                for (int i = 0; i < 60; i++)
                {
                    // 每分钟检测一次
                    if (!this.m_thread.IsAlive) break;
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            ((AsyncUserToken)e.UserToken).ActiveDateTime = DateTime.Now;
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    this.ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    this.ProcessSend(e);
                    break;
                default:
                    this.CloseClientSocket(e);
                    LogHelper.Log.InfoFormat(
                        "客户端{0}:不支持的Socket操作",
                        ((AsyncUserToken)e.UserToken).Socket.RemoteEndPoint.ToString());
                    break;
            }
        }

        /// <summary>
        /// 解析命令
        /// </summary>
        /// <param name="token"></param>
        private void ParseCommand(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            LogHelper.Log.InfoFormat("客户端{0}:In ParseCommand", token.Socket.RemoteEndPoint.ToString());
            if (e.SocketError == SocketError.Success)
            {
                bool willRaiseEvent = false;
                switch (token.BasicPackage.CID)
                {
                    case FuncCode.Bootstrap:
                        e.SetBuffer(e.Offset, PackgeSize.MacBootstrapPackageSize);
                        token.CommandType = CommandType.Bootstrap;
                        willRaiseEvent = token.Socket.ReceiveAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessReceive(e);
                        }

                        break;
                    case FuncCode.Login:
                        e.SetBuffer(e.Offset, PackgeSize.LoginPackageSize - PackgeSize.BasicPackageSize);
                        token.CommandType = CommandType.Login;
                        willRaiseEvent = token.Socket.ReceiveAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessReceive(e);
                        }

                        break;
                    case FuncCode.EnumData:
                        e.SetBuffer(e.Offset, PackgeSize.LoginPackageSize - PackgeSize.BasicPackageSize);
                        token.CommandType = CommandType.EnumDataCount;
                        willRaiseEvent = token.Socket.ReceiveAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessReceive(e);
                        }

                        break;
                    case FuncCode.PushDataRequest:
                        e.SetBuffer(e.Offset, PackgeSize.LoginPackageSize - PackgeSize.BasicPackageSize);
                        token.CommandType = CommandType.PushDataRequest;
                        willRaiseEvent = token.Socket.ReceiveAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessReceive(e);
                        }

                        break;
                    case FuncCode.PushData:
                        e.SetBuffer(e.Offset, PackgeSize.LoginPackageSize - PackgeSize.BasicPackageSize);
                        token.CommandType = CommandType.PushDataCount;
                        willRaiseEvent = token.Socket.ReceiveAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessReceive(e);
                        }

                        break;
                    case FuncCode.NullCommand:
                        BasicPackage package7 = new BasicPackage();
                        package7.PID = SocketHelper.HostToNetwork((ushort)0x0000);
                        package7.CID = SocketHelper.HostToNetwork(FuncCode.NullCommandReply);
                        package7.ST = 0;
                        package7.UID = Convert.ToByte(token.UserName);
                        package7.VER = SocketHelper.HostToNetwork((ushort)0x1000);
                        var result = SocketHelper.StructToBytes(package7);
                        token.CommandType = CommandType.NullCommand;
                        e.SetBuffer(e.Offset, 8);
                        Buffer.BlockCopy(result, e.Offset, e.Buffer, 0, 8);
                        LogHelper.Log.InfoFormat(
                            "向客户端{0}发送心跳包回复报文，长度为{1}",
                            token.Socket.RemoteEndPoint.ToString(),
                            result.Length);
                        willRaiseEvent = token.Socket.SendAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessSend(e);
                        }

                        break;
                    case FuncCode.Close:
                        this.CloseClientSocket(e);
                        break;
                    default: break;
                }
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }

        /// <summary>
        /// 处理Accept逻辑
        /// </summary>
        /// <param name="e"></param>
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                Interlocked.Increment(ref this.m_numConnectedSockets);
                LogHelper.Log.InfoFormat(
                    "来自{0}的连接,当前连接数量为:{1}...",
                    e.AcceptSocket.RemoteEndPoint.ToString(),
                    this.m_numConnectedSockets);

                SocketAsyncEventArgs readEventArgs = this.m_readWritePool.Pop();
                ((AsyncUserToken)readEventArgs.UserToken).Socket = e.AcceptSocket;
                ((AsyncUserToken)readEventArgs.UserToken).CommandType = CommandType.Ensure;

                this.m_UsedObjects.Add(readEventArgs);

                EnsurePackage package = new EnsurePackage();
                package.PID = SocketHelper.HostToNetwork((ushort)0x0000);
                package.CID = SocketHelper.HostToNetwork(FuncCode.Ensure);
                package.ST = 0;
                package.UID = 0;
                package.VER = SocketHelper.HostToNetwork((ushort)0x1000);
                package.IPAddress = string.Empty.PadRight(20, '\0');
                package.Port = SocketHelper.HostToNetwork((uint)0);

                var result = SocketHelper.StructToBytes(package);
                readEventArgs.SetBuffer(e.Offset, PackgeSize.EnsurePackageSize);
                Buffer.BlockCopy(result, e.Offset, readEventArgs.Buffer, 0, PackgeSize.EnsurePackageSize);
                LogHelper.Log.InfoFormat(
                    "向客户端{0}发送确认报文,发送字节长度{1}",
                    ((AsyncUserToken)readEventArgs.UserToken).Socket.RemoteEndPoint.ToString(),
                    result.Length);
                bool willRaiseEvent = e.AcceptSocket.SendAsync(readEventArgs);
                if (!willRaiseEvent)
                {
                    this.ProcessSend(readEventArgs);
                }

                LogHelper.Log.InfoFormat("等待下一个连接");
                this.StartAccept(e);
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }

        /// <summary>
        /// ProcessReceive
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            LogHelper.Log.InfoFormat("客户端{0}:In ProcessReceive", token.Socket.RemoteEndPoint.ToString());
            if (e.SocketError == SocketError.Success)
            {
                switch (token.CommandType)
                {
                    case CommandType.PackagePrefix:
                        token.Length = 8;
                        this.ProcessReceivePackagePrefix(e);
                        break;
                    case CommandType.Bootstrap:
                        token.Length = 32;
                        this.ProcessReceiveBootstrap(e);
                        break;
                    case CommandType.Login:
                        token.Length = 8;
                        this.ProcessReceiveLogin(e);
                        break;
                    case CommandType.EnumDataCount:
                        token.Length = 8;
                        this.ProcessReceiveEnumDataCount(e);
                        break;
                    case CommandType.EnumDataRecord:
                        token.Length = 104;
                        this.ProcessReceiveEnumDataRecord(e);
                        break;
                    case CommandType.PushDataRequest:
                        token.Length = 8;
                        this.ProcessReceivePushDataRequest(e);
                        break;
                    case CommandType.PushDataCount:
                        token.Length = 8;
                        this.ProcessReceivePushDataCount(e);
                        break;
                    case CommandType.PushDataRecordHead:
                        token.Length = 8;
                        this.ProcessReceivePushDataRecordHead(e);
                        break;
                    case CommandType.PushDataRecordBody:
                        this.ProcessReceivePushDataRecordBody(e);
                        break;
                    case CommandType.Close:
                        this.CloseClientSocket(e);
                        break;
                    case CommandType.NullCommand:
                        token.CommandType = CommandType.NullCommandReply;
                        this.ProcessSend(e);
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// 设备自举
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceiveBootstrap(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            LogHelper.Log.InfoFormat("客户端{0}:In ProcessReceivePackagePrefix", token.Socket.RemoteEndPoint.ToString());
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                for (int i = e.Offset; i < e.BytesTransferred; i++)
                {
                    token.resultBytes.Add(e.Buffer[i]);
                }

                if (token.resultBytes.Count < token.Length)
                {
                    e.SetBuffer(e.Offset, token.Length - token.resultBytes.Count);
                    token.CommandType = CommandType.Bootstrap;
                    bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                    if (!willRaiseEvent)
                    {
                        this.ProcessReceive(e);
                    }
                }
                else
                {
                    MacBootstrapPackage package =
                        SocketHelper.BytesToStruct<MacBootstrapPackage>(token.resultBytes.ToArray());
                    token.Machine.MachineCode = SocketHelper.BinToHexString(package.DID).TrimEnd().ToUpper();
                    LogHelper.Log.InfoFormat(
                        "客户端{0}:接收到自举设备编号{1}",
                        token.Socket.RemoteEndPoint.ToString(),
                        token.Machine.MachineCode);
                    token.resultBytes.Clear();
                    if (token.MongoHelper.GetMacLoginInfo(token) == 1)
                    {
                        LoginPackage package4 = new LoginPackage();
                        package4.PID = SocketHelper.HostToNetwork((ushort)0x0000);
                        package4.CID = SocketHelper.HostToNetwork(FuncCode.Login);
                        package4.ST = 0;
                        package4.UID = Convert.ToByte(token.UserName);
                        package4.VER = SocketHelper.HostToNetwork((ushort)0x1000);
                        package4.Password = token.Password.PadRight(8, '\0');
                        token.CommandType = CommandType.Login;
                        var result = SocketHelper.StructToBytes(package4);
                        e.SetBuffer(e.Offset, 16);
                        Buffer.BlockCopy(result, e.Offset, e.Buffer, 0, 16);
                        LogHelper.Log.InfoFormat(
                            "向客户端{0}:发送登录请求报文,长度为{1}",
                            token.Socket.RemoteEndPoint.ToString(),
                            result.Length);
                        bool willRaiseEvent = token.Socket.SendAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessSend(e);
                        }
                    }
                    else
                    {
                        this.CloseClientSocket(e);
                        LogHelper.Log.InfoFormat("没有该设备的登录信息或设备已停用");
                    }
                }
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }

        /// <summary>
        /// 解析枚举数据项个数部分
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceiveEnumDataCount(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            LogHelper.Log.InfoFormat("客户端{0}:In ProcessReceiveEnumDataCount", token.Socket.RemoteEndPoint.ToString());
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                for (int i = e.Offset; i < e.BytesTransferred; i++)
                {
                    token.resultBytes.Add(e.Buffer[i]);
                }

                if (token.resultBytes.Count < token.Length)
                {
                    e.SetBuffer(e.Offset, token.Length - token.resultBytes.Count);
                    token.CommandType = CommandType.EnumDataCount;
                    bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                    if (!willRaiseEvent)
                    {
                        this.ProcessReceive(e);
                    }
                }
                else
                {
                    EnumPackage package = SocketHelper.BytesToStruct<EnumPackage>(token.resultBytes.ToArray());
                    package.Count = SocketHelper.NetworkToHost(package.Count);
                    token.resultBytes.Clear();
                    token.MappedFieldCount = package.Count;
                    LogHelper.Log.InfoFormat(
                        "客户端{0}:接收到{1}个枚举数据",
                        token.Socket.RemoteEndPoint.ToString(),
                        token.MappedFieldCount);
                    token.Size = PackgeSize.EnumDataPackageSize * token.MappedFieldCount;
                    if (token.Size > 1024 * 10)
                    {
                        this.CloseClientSocket(e);
                        LogHelper.Log.InfoFormat("非法数据包,大小为:" + token.Size + string.Empty);
                    }
                    else
                    {
                        e.SetBuffer(e.Offset, PackgeSize.EnumDataPackageSize);
                        token.CommandType = CommandType.EnumDataRecord;
                        bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessReceive(e);
                        }
                    }
                }
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }

        /// <summary>
        /// 解析具体枚举数据项
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceiveEnumDataRecord(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            LogHelper.Log.InfoFormat("客户端{0}:In ProcessReceiveEnumDataRecord", token.Socket.RemoteEndPoint.ToString());
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                for (int i = e.Offset; i < e.BytesTransferred; i++)
                {
                    token.resultBytes.Add(e.Buffer[i]);
                }

                if (token.resultBytes.Count < token.Length)
                {
                    e.SetBuffer(e.Offset, token.Length - token.resultBytes.Count);
                    token.CommandType = CommandType.EnumDataRecord;
                    bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                    if (!willRaiseEvent)
                    {
                        this.ProcessReceive(e);
                    }
                }
                else
                {
                    EnumDataRecordPackage package =
                        SocketHelper.BytesToStruct<EnumDataRecordPackage>(token.resultBytes.ToArray());
                    package.DID = SocketHelper.NetworkToHost(package.DID);
                    token.Machine.Map.MapArray.Add(
                        new MapArray() { Id = package.DID.ToString(), Value = package.NAME });
                    LogHelper.Log.InfoFormat(
                        "客户端{0}:接收枚举数据项编号{1},名称{2}",
                        token.Socket.RemoteEndPoint.ToString(),
                        package.DID.ToString(),
                        package.NAME);
                    token.InsertedCount++;
                    token.resultBytes.Clear();
                    if (token.InsertedCount < token.MappedFieldCount)
                    {
                        e.SetBuffer(e.Offset, PackgeSize.EnumDataPackageSize);
                        token.CommandType = CommandType.EnumDataRecord;
                        bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessReceive(e);
                        }
                    }
                    else
                    {
                        token.InsertedCount = 0;
                        if (token.MongoHelper.InsertMappedFiled(token.Machine) == 1)
                        {
                            RequestPushPackage package6 = new RequestPushPackage();
                            package6.PID = SocketHelper.HostToNetwork((ushort)0x0000);
                            package6.CID = SocketHelper.HostToNetwork(FuncCode.PushDataRequest);
                            package6.UID = (byte)token.UserName;
                            package6.ST = 0;
                            package6.VER = SocketHelper.HostToNetwork((ushort)0x1000);
                            package6.RV = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
                            token.CommandType = CommandType.PushDataRequest;
                            var result = SocketHelper.StructToBytes(package6);
                            e.SetBuffer(e.Offset, PackgeSize.RequestPushPackageSize);
                            Buffer.BlockCopy(result, e.Offset, e.Buffer, 0, PackgeSize.RequestPushPackageSize);
                            LogHelper.Log.InfoFormat(
                                "向客户端{0}:发送推送数据请求报文,长度为{1}",
                                token.Socket.RemoteEndPoint.ToString(),
                                result.Length);
                            bool willRaiseEvent = token.Socket.SendAsync(e);
                            if (!willRaiseEvent)
                            {
                                this.ProcessSend(e);
                            }
                        }
                        else
                        {
                            this.CloseClientSocket(e);
                        }
                    }
                }
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }

        /// <summary>
        /// 设备登录
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceiveLogin(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            LogHelper.Log.InfoFormat("客户端{0}:In ProcessReceiveLogin", token.Socket.RemoteEndPoint.ToString());
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                if (token.BasicPackage.ST == 0)
                {
                    for (int i = e.Offset; i < e.BytesTransferred; i++)
                    {
                        token.resultBytes.Add(e.Buffer[i]);
                    }

                    if (token.resultBytes.Count < token.Length)
                    {
                        e.SetBuffer(e.Offset, token.Length - token.resultBytes.Count);
                        token.CommandType = CommandType.Login;
                        bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessReceive(e);
                        }
                    }
                    else
                    {
                        token.IsLogined = true;
                        token.resultBytes.Clear();
                        BasicPackage package5 = new BasicPackage();
                        package5.PID = SocketHelper.HostToNetwork((ushort)0x0000);
                        package5.CID = SocketHelper.HostToNetwork(FuncCode.EnumData);
                        package5.ST = 0;
                        package5.UID = Convert.ToByte(token.UserName);
                        package5.VER = SocketHelper.HostToNetwork((ushort)0x1000);
                        token.CommandType = CommandType.EnumData;
                        var result = SocketHelper.StructToBytes(package5);
                        e.SetBuffer(e.Offset, 8);
                        Buffer.BlockCopy(result, e.Offset, e.Buffer, 0, 8);
                        LogHelper.Log.InfoFormat(
                            "向客户端{0}:发送枚举数据请求报文,长度为{1}",
                            token.Socket.RemoteEndPoint.ToString(),
                            result.Length);
                        bool willRaiseEvent = token.Socket.SendAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessSend(e);
                        }
                    }
                }
                else
                {
                    this.CloseClientSocket(e);
                }
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }

        /// <summary>
        /// 解析8字节命令部分
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceivePackagePrefix(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            LogHelper.Log.InfoFormat("客户端{0}:In ProcessReceivePackagePrefix", token.Socket.RemoteEndPoint.ToString());
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                for (int i = e.Offset; i < e.BytesTransferred; i++)
                {
                    token.resultBytes.Add(e.Buffer[i]);
                }

                if (token.resultBytes.Count < token.Length)
                {
                    e.SetBuffer(e.Offset, token.Length - token.resultBytes.Count);
                    token.CommandType = CommandType.PackagePrefix;
                    bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                    if (!willRaiseEvent)
                    {
                        this.ProcessReceive(e);
                    }
                }
                else
                {
                    BasicPackage package = SocketHelper.BytesToStruct<BasicPackage>(token.resultBytes.ToArray());
                    package.PID = SocketHelper.NetworkToHost(package.PID);
                    package.CID = SocketHelper.NetworkToHost(package.CID);
                    package.VER = SocketHelper.NetworkToHost(package.VER);
                    token.resultBytes.Clear();
                    if (package.VER == FuncCode.ALPVERSION)
                    {
                        token.BasicPackage = package;
                        this.ParseCommand(e);
                    }
                    else
                    {
                        this.CloseClientSocket(e);
                        LogHelper.Log.InfoFormat("协议版本错误，当前版为:{0},需求版本为：{1}", package.VER, FuncCode.ALPVERSION);
                    }
                }
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }

        /// <summary>
        /// 处理接收推送数据项的个数部分
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceivePushDataCount(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            LogHelper.Log.InfoFormat("客户端{0}:In ProcessReceivePushDataCount", token.Socket.RemoteEndPoint.ToString());
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                for (int i = e.Offset; i < e.BytesTransferred; i++)
                {
                    token.resultBytes.Add(e.Buffer[i]);
                }

                if (token.resultBytes.Count < token.Length)
                {
                    e.SetBuffer(e.Offset, token.Length - token.resultBytes.Count);
                    token.CommandType = CommandType.PushDataCount;
                    bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                    if (!willRaiseEvent)
                    {
                        this.ProcessReceive(e);
                    }
                }
                else
                {
                    PushHeadPackage package = SocketHelper.BytesToStruct<PushHeadPackage>(token.resultBytes.ToArray());
                    package.Count = SocketHelper.NetworkToHost(package.Count);
                    token.CollectDataCount = package.Count;
                    LogHelper.Log.InfoFormat(
                        "客户端{0}:接收到{1}个推送数据",
                        token.Socket.RemoteEndPoint.ToString(),
                        token.CollectDataCount);
                    token.resultBytes.Clear();
                    if (package.Count == 0)
                    {
                        token.CommandType = CommandType.PackagePrefix;
                    }
                    else
                    {
                        token.CommandType = CommandType.PushDataRecordHead;
                    }

                    e.SetBuffer(e.Offset, PackgeSize.BasicPackageSize);
                    bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                    if (!willRaiseEvent)
                    {
                        this.ProcessReceive(e);
                    }
                }
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }

        /// <summary>
        /// 获取数据项的值部分
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceivePushDataRecordBody(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            LogHelper.Log.InfoFormat(
                "客户端{0}:In ProcessReceivePushDataRecordBody",
                token.Socket.RemoteEndPoint.ToString());
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                for (int i = e.Offset; i < e.BytesTransferred; i++)
                {
                    token.resultBytes.Add(e.Buffer[i]);
                }

                if (token.resultBytes.Count < token.Length)
                {
                    e.SetBuffer(e.Offset, token.Length - token.resultBytes.Count);
                    token.CommandType = CommandType.PushDataRecordBody;
                    bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                    if (!willRaiseEvent)
                    {
                        this.ProcessReceive(e);
                    }
                }
                else
                {
                    token.Value = token.resultBytes.ToArray();
                    var item = new DataItem() { Id = token.DID, Type = token.DT, Value = token.Value };
                    LogHelper.Log.InfoFormat(
                        "客户端{0}:接收到数据项编号 - {1},类型 - {2},值 - {3}",
                        token.Socket.RemoteEndPoint.ToString(),
                        token.DID,
                        token.DT,
                        SocketHelper.GetValueByType(item));
                    token.DataItems.Add(item);
                    token.InsertedCount++;
                    token.resultBytes.Clear();
                    if (token.InsertedCount < token.CollectDataCount)
                    {
                        token.CommandType = CommandType.PushDataRecordHead;
                        e.SetBuffer(e.Offset, PackgeSize.BasicPackageSize);
                        bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessReceive(e);
                        }
                    }
                    else
                    {
                        if (token.MongoHelper.InsertValueToMongo(token.Machine, token.DataItems) == 1)
                        {
                            LogHelper.Log.InfoFormat(
                                "客户端{0}:所有推送数据已接收,等待下一次推送",
                                token.Socket.RemoteEndPoint.ToString());
                            token.DataItems.Clear();
                            token.InsertedCount = 0;
                            LogHelper.Log.InfoFormat("客户端{0}:发送推送数据回复", token.Socket.RemoteEndPoint.ToString());
                            token.CommandType = CommandType.PushDataReply;
                            BasicPackage package8 = new BasicPackage();
                            package8.PID = SocketHelper.HostToNetwork((ushort)0x0000);
                            package8.CID = SocketHelper.HostToNetwork(FuncCode.PushDataReply);
                            package8.ST = 0;
                            package8.UID = Convert.ToByte(token.UserName);
                            package8.VER = SocketHelper.HostToNetwork((ushort)0x1000);
                            var result = SocketHelper.StructToBytes(package8);
                            e.SetBuffer(e.Offset, 8);
                            Buffer.BlockCopy(result, e.Offset, e.Buffer, 0, 8);
                            LogHelper.Log.InfoFormat(
                                "向客户端{0}:发送登录请求报文,长度为{1}",
                                token.Socket.RemoteEndPoint.ToString(),
                                result.Length);
                            bool willRaiseEvent = token.Socket.SendAsync(e);
                            if (!willRaiseEvent)
                            {
                                this.ProcessSend(e);
                            }
                        }
                        else
                        {
                            this.CloseClientSocket(e);
                        }
                    }
                }
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }

        /// <summary>
        /// 处理推送的数据项记录前8个字节获取数据内容的长度
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceivePushDataRecordHead(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            LogHelper.Log.InfoFormat(
                "客户端{0}:In ProcessReceivePushDataRecordHead",
                token.Socket.RemoteEndPoint.ToString());
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                for (int i = e.Offset; i < e.BytesTransferred; i++)
                {
                    token.resultBytes.Add(e.Buffer[i]);
                }

                if (token.resultBytes.Count < token.Length)
                {
                    e.SetBuffer(e.Offset, token.Length - token.resultBytes.Count);
                    token.CommandType = CommandType.PushDataRecordHead;
                    bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                    if (!willRaiseEvent)
                    {
                        this.ProcessReceive(e);
                    }
                }
                else
                {
                    PushBodyPackage package = SocketHelper.BytesToStruct<PushBodyPackage>(token.resultBytes.ToArray());
                    package.DID = SocketHelper.NetworkToHost(package.DID);
                    package.DLEN = SocketHelper.NetworkToHost(package.DLEN);
                    token.DID = package.DID;
                    token.Length = package.DLEN;
                    token.DT = package.DT;
                    token.resultBytes.Clear();
                    token.CommandType = CommandType.PushDataRecordBody;
                    e.SetBuffer(e.Offset, token.Length);
                    LogHelper.Log.InfoFormat(
                        "客户端{0}:接收到数据项值长度为{1}",
                        token.Socket.RemoteEndPoint.ToString(),
                        token.Length);
                    bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                    if (!willRaiseEvent)
                    {
                        this.ProcessReceive(e);
                    }
                }
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }

        /// <summary>
        /// 解析推送数据请求返回结果
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceivePushDataRequest(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            LogHelper.Log.InfoFormat("客户端{0}:In ProcessReceivePushDataRequest", token.Socket.RemoteEndPoint.ToString());
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                for (int i = e.Offset; i < e.BytesTransferred; i++)
                {
                    token.resultBytes.Add(e.Buffer[i]);
                }

                if (token.resultBytes.Count < token.Length)
                {
                    e.SetBuffer(e.Offset, token.Length - token.resultBytes.Count);
                    token.CommandType = CommandType.PushDataRequest;
                    bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                    if (!willRaiseEvent)
                    {
                        this.ProcessReceive(e);
                    }
                }
                else
                {
                    if (token.BasicPackage.ST == 0)
                    {
                        LogHelper.Log.InfoFormat("客户端{0}:支持推送数据", token.Socket.RemoteEndPoint.ToString());
                        token.resultBytes.Clear();
                        e.SetBuffer(e.Offset, PackgeSize.BasicPackageSize);
                        token.CommandType = CommandType.PackagePrefix;
                        bool willRaiseEvent = token.Socket.ReceiveAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessReceive(e);
                        }
                    }
                    else
                    {
                        ErrorPackage package = SocketHelper.BytesToStruct<ErrorPackage>(token.resultBytes.ToArray());
                        package.ERR = SocketHelper.NetworkToHost(package.ERR);
                        package.RV = SocketHelper.NetworkToHost(package.RV);
                        LogHelper.Log.InfoFormat(
                            "客户端{0}:出现错误,错误码为{1}",
                            token.Socket.RemoteEndPoint.ToString(),
                            package.ERR);
                        this.CloseClientSocket(e);
                    }
                }
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            LogHelper.Log.InfoFormat("客户端{0}:In ProcessSend", token.Socket.RemoteEndPoint.ToString());
            if (e.SocketError == SocketError.Success)
            {
                bool willRaiseEvent = false;
                switch (token.CommandType)
                {
                    case CommandType.Ensure:
                    case CommandType.Login:
                    case CommandType.EnumData:
                    case CommandType.PushDataRequest:
                    case CommandType.NullCommand:
                    case CommandType.PushDataReply:
                        token.CommandType = CommandType.PackagePrefix;
                        e.SetBuffer(e.Offset, PackgeSize.BasicPackageSize);
                        willRaiseEvent = token.Socket.ReceiveAsync(e);
                        if (!willRaiseEvent)
                        {
                            this.ProcessReceive(e);
                        }

                        break;
                    case CommandType.Close:
                        this.CloseClientSocket(e);
                        break;
                    default: break;
                }
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }
    }
}