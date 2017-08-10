namespace SocketAsyncEventArgsServer
{
    public class PackgeSize
    {
        // 连接确认Size
        public const int EnsurePackageSize = 32;

        // 枚举数据项Size(减去8字节命令部分)
        public const int EnumDataPackageSize = 104;

        // 返回结果Size(减去8字节命令部分)
        public const int ErrorPackageSize = 16;

        // 登录包Size
        public const int LoginPackageSize = 16;

        // 设备自举包Size(减去8字节命令部分)
        public const int MacBootstrapPackageSize = 32;

        // 请求推送包Size(减去8字节命令部分)
        public const int RequestPushPackageSize = 16;

        public static int BasicPackageSize = 8;
    }
}