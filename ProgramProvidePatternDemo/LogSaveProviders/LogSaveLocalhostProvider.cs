namespace ProgramProvidePatternDemo
{
    using System;

    public class LogSaveLocalhostProvider : LogSaveBaseProvider
    {
        protected override bool DoSave(LogEntity logEntity)
        {
            Console.WriteLine(logEntity.Content.LogInfo);
            return true;
        }

        protected override void FormatLogContent(LogEntity logEntity)
        {
            logEntity.Content.LogInfo = logEntity.Content.LogTrackInfo + " - " + logEntity.Content.LogInfo;
        }

        protected override bool ValidateLogEntity(LogEntity logEntity)
        {
            var result = false;
            if (base.ValidateLogEntity(logEntity))
            {
                if (!string.IsNullOrEmpty(logEntity.Content.LogTrackInfo))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}