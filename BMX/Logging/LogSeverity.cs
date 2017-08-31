namespace Bmx.Abp.Logging
{
    using System;

    /// <summary>
    /// Indicates severity for log.
    /// </summary>
    [Flags]
    public enum LogSeverity
    {
        /// <summary>
        /// Debug.
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Info.
        /// </summary>
        Info = 2,

        /// <summary>
        /// Warn.
        /// </summary>
        Warn = 4,

        /// <summary>
        /// Error.
        /// </summary>
        Error = 8,

        /// <summary>
        /// Fatal.
        /// </summary>
        Fatal = 16
    }
}
