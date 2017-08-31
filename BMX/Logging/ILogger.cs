﻿namespace Bmx.Abp.Logging
{
    public interface ILogger
    {
        void Debug(string content);

        void Info(string content);

        void Warn(string content);

        void Error(string content);
    }
}
