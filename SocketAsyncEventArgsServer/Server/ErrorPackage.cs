namespace SocketAsyncEventArgs.Server
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// 错误包结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ErrorPackage
    {
        /// </summary>
        public uint ERR;

        /// <summary>
        /// ：4字节，保留取常值 0x000000000
        /// </summary>
        public uint RV;
    }
}