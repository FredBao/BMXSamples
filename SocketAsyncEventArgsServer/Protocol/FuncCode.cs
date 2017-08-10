namespace SocketAsyncEventArgsServer
{
    /// <summary>
    /// 功能码表
    /// </summary>
    public static class FuncCode
    {
        /// <summary>
        /// 协议版本号
        /// </summary>
        public const ushort ALPVERSION = 0x1000;

        /// <summary>
        /// 设备自举
        /// </summary>
        public const ushort Bootstrap = 0xffff;

        /// <summary>
        /// 4.关闭连接
        /// </summary>
        public const ushort Close = 0xfffd;

        /// <summary>
        /// 2.正常确认连接
        /// </summary>
        public const ushort Ensure = 0xfffb;

        /// <summary>
        /// 8.枚举所有数据
        /// </summary>
        public const ushort EnumData = 0xff04;

        /// <summary>
        /// 1.登录
        /// </summary>
        public const ushort Login = 0xfffe;

        /// <summary>
        /// 空指令
        /// </summary>
        public const ushort NullCommand = 0x0000;

        // <summary>
        /// 空指令回复
        /// </summary>
        public const ushort NullCommandReply = 0x8000;

        /// <summary>
        /// 11.服务器推送数据
        /// </summary>
        public const ushort PushData = 0xf006;

        /// <summary>
        /// 18.推送数据回复
        /// </summary>
        public const ushort PushDataReply = 0xfa06;

        /// <summary>
        /// 10.服务器推送数据请求
        /// </summary>
        public const ushort PushDataRequest = 0xff06;

        /// <summary>
        /// 5.读取数据
        /// </summary>
        public const ushort Read = 0xff01;

        /// <summary>
        /// 12.读取数据带参数
        /// </summary>
        public const ushort ReadWithParam = 0xff07;

        /// <summary>
        /// 13.重启设备
        /// </summary>
        public const ushort Reboot = 0x5242;

        /// <summary>
        /// 3.重定向
        /// </summary>
        public const ushort Redirect = 0xfffc;

        /// <summary>
        /// 7.请求所有数据
        /// </summary>
        public const ushort RequestForAllData = 0xff03;

        /// <summary>
        /// 16.更新设备配置信息
        /// </summary>
        public const ushort UpdateDevConfig = 0x4443;

        /// <summary>
        /// 17.更新设备软件系统
        /// </summary>
        public const ushort UpdateDevSys = 0x5553;

        /// <summary>
        /// 15.上传设备基础信息
        /// </summary>
        public const ushort UploadDevBasicInfo = 0x5549;

        /// <summary>
        /// 14.上传设备配置
        /// </summary>
        public const ushort UploadDevConfig = 0x5543;

        /// <summary>
        /// 9.写入数据
        /// </summary>
        public const ushort Write = 0xff05;

        /// <summary>
        /// 6.写入数据 (带参数输入)
        /// </summary>
        public const ushort WriteWithParam = 0xff02;
    }
}