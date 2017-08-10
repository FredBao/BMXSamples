namespace SocketAsyncEventArgsServer
{
    /// <summary>
    /// 错误码表
    /// </summary>
    public static class ErrorCode
    {
        /// <summary>
        /// 用户认证错误
        /// </summary>
        public const uint AuthenticationException = 0x80000004;

        /// <summary>
        /// 数据格式错误
        /// </summary>
        public const uint FormatException = 0x80000006;

        /// <summary>
        /// 内部系统错误
        /// </summary>
        public const uint InnerException = 0xFFFFFFFF;

        /// <summary>
        /// 不支持的功能码
        /// </summary>
        public const uint NotSupported = 0x80000001;

        /// <summary>
        /// 参数错误
        /// </summary>
        public const uint ParamException = 0x80000002;

        /// <summary>
        /// 数据包解析错误
        /// </summary>
        public const uint ParseExcetpion = 0x80000003;

        /// <summary>
        /// 未找到数据项
        /// </summary>
        public const uint RecordNotFound = 0x80000005;

        /// <summary>
        /// 无错误，成功响应
        /// </summary>
        public const uint Succeed = 0x80000000;
    }
}