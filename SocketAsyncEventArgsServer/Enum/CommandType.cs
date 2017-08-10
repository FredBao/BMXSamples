namespace SocketAsyncEventArgsServer
{
    public enum CommandType
    {
        /// <summary>
        /// 8字节命令
        /// </summary>
        PackagePrefix,

        Ensure,

        Bootstrap,

        Login,

        EnumData,

        EnumDataCount,

        EnumDataRecord,

        PushDataRequest,

        PushData,

        PushDataCount,

        PushDataRecordHead,

        PushDataRecordBody,

        PushDataReply,

        Close,

        NullCommand,

        NullCommandReply
    }

    public enum ErrorLevel
    {
        /// <summary>
        /// 会话内错误，只需要记录到库里
        /// </summary>
        Level1,

        /// <summary>
        /// 程序错误，需要记录到库里，可以考虑发送邮件
        /// </summary>
        Level2
    }

    public enum ConfigField
    {
        Unknow, // 未知

        AlarmCode, // (报警编号)

        AlarmMsg, // (报警内容)

        StateCode, // (状态编号)

        CapacityCount, // （计数器显示值）

        ProgramName // 程序号
    }
}