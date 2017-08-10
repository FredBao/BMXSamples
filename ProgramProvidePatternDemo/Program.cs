namespace ProgramProvidePatternDemo
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.Type = LogType.Error;
            logEntity.Level = LogLevel.Gravenss;
            logEntity.Content = new LogContent() { LogTrackInfo = "Program.Main", LogInfo = "字符串不能为空" };
            ILogSaveProvider provider = new LogSaveLocalhostProvider(); // can use IOC pattern replace this logic
            provider.SaveLog(logEntity);
            Console.ReadKey();
        }
    }
}