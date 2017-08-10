namespace SocketAsyncEventArgsServer
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// 8字节的基础命令报文
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct BasicPackage
    {
        /// <summary>
        /// 2字节，请求标识，该字段由发起端给出回复时需要使用值填充
        /// </summary>
        public ushort PID;

        /// <summary>
        /// 2字节，命令标识取
        /// </summary>
        public ushort CID;

        /// <summary>
        /// 1字节，用户标识硬件设备中置的ID
        /// </summary>
        public byte UID;

        /// <summary>
        /// 1字节，取常值 1
        /// </summary>
        public byte ST;

        /// <summary>
        /// 2字节，协议版本号 1000
        /// </summary>
        public ushort VER;
    };

    /// <summary>
    /// 确认报文
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct EnsurePackage
    {
        /// <summary>
        /// 2字节，请求标识，该字段由发起端给出回复时需要使用值填充
        /// </summary>
        public ushort PID;

        /// <summary>
        /// 2字节，命令标识取
        /// </summary>
        public ushort CID;

        /// <summary>
        /// 1字节，用户标识硬件设备中置的ID
        /// </summary>
        public byte UID;

        /// <summary>
        /// 1字节，取常值 1
        /// </summary>
        public byte ST;

        /// <summary>
        /// 2字节，协议版本号 1000
        /// </summary>
        public ushort VER;

        /// <summary>
        /// 重定向IP
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string IPAddress;

        /// <summary>
        /// 重定向Port
        /// </summary>
        public uint Port;
    };

    /// <summary>
    /// 登录报文
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct LoginPackage
    {
        /// <summary>
        /// 2字节，请求标识，该字段由发起端给出回复时需要使用值填充
        /// </summary>
        public ushort PID;

        /// <summary>
        /// 2字节，命令标识取
        /// </summary>
        public ushort CID;

        /// <summary>
        /// 1字节，用户标识硬件设备中置的ID
        /// </summary>
        public byte UID;

        /// <summary>
        /// 1字节，取常值 1
        /// </summary>
        public byte ST;

        /// <summary>
        /// 2字节，协议版本号 1000
        /// </summary>
        public ushort VER;

        /// <summary>
        /// 登录密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Password;
    }

    /// <summary>
    /// 验证报文
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct LoginResultPackage
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public uint ERR;

        /// <summary>
        /// 保留字
        /// </summary>
        public uint RV;
    }

    /// <summary>
    /// 设备自举报文
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MacBootstrapPackage
    {
        /// <summary>
        /// 32 字节数据块，设备ID
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] DID;
    }

    /// <summary>
    /// 枚举映射报文
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct EnumPackage
    {
        /// <summary>
        /// 数据项个数
        /// </summary>
        public ushort Count;

        /// <summary>
        /// 6 字节数据块，未使用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] RV;
    };

    /// <summary>
    /// 枚举映射数据项报文
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct EnumDataRecordPackage
    {
        /// <summary>
        /// 2 字节整数，数据项标识
        /// </summary>
        public ushort DID;

        /// <summary>
        /// DT：1 字节整数，数据类别,最高位表示是否可写入[0=只读，1=读写]，其它位数值表示数据类别[0=字符串,1=双精度浮
        /// 点数,2=整数(32 位),3=字节,4=布尔值,5=字节块]
        /// </summary>
        public byte DT;

        /// <summary>
        /// 
        /// </summary>
        public byte DRV;

        /// <summary>
        /// NAME：16 字节字符串，不足16 字节部分填充0，数据项名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string NAME;

        /// <summary>
        /// ADDR：64 字节字符串，不足64 字节部分填充0，数据地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string ADDR;

        /// <summary>
        /// RATE：4 字节单精度浮点数，数据比率 
        /// </summary>
        [MarshalAs(UnmanagedType.R4, SizeConst = 4)]
        public float RATE;

        /// <summary>
        /// UPL：8 字节双精度浮点数，数据上限
        /// </summary>
        [MarshalAs(UnmanagedType.R8, SizeConst = 8)]
        public double UPL;

        /// <summary>
        /// LOL：8 字节双精度浮点数，数据下限
        /// </summary>
        [MarshalAs(UnmanagedType.R8, SizeConst = 8)]
        public double LOL;
    };

    /// <summary>
    /// 请求推送数据报文
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct RequestPushPackage
    {
        /// <summary>
        /// 2字节，请求标识，该字段由发起端给出回复时需要使用值填充
        /// </summary>
        public ushort PID;

        /// <summary>
        /// 2字节，命令标识取
        /// </summary>
        public ushort CID;

        /// <summary>
        /// 1字节，用户标识硬件设备中置的ID
        /// </summary>
        public byte UID;

        /// <summary>
        /// 1字节，取常值 1
        /// </summary>
        public byte ST;

        /// <summary>
        /// 2字节，协议版本号 1000
        /// </summary>
        public ushort VER;

        /// <summary>
        /// 8字节 数据块,未使用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] RV;
    };

    /// <summary>
    /// 推送数据头报文
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PushHeadPackage
    {
        /// <summary>
        /// 数据项个数
        /// </summary>
        public ushort Count;

        /// <summary>
        /// 6 字节数据块，未使用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] RV;
    };

    /// <summary>
    /// 推送数据body报文
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PushBodyPackage
    {
        /// <summary>
        /// 2 字节整数，数据项标识
        /// </summary>
        public ushort DID;

        /// <summary>
        /// DLENDLENDLEN：2字节 整数 ，数据项值长度 ，数据项值长度 ，数据项值长度
        /// </summary>
        public ushort DLEN;

        /// <summary>
        /// DT：1 字节整数，数据类别,最高位表示是否可写入[0=只读，1=读写]，其它位数值表示数据类别[0=字符串,1=双精度浮
        /// 点数,2=整数(32 位),3=字节,4=布尔值,5=字节块]
        /// </summary>
        public byte DT;

        /// <summary>
        /// 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] DRV;
    };
}