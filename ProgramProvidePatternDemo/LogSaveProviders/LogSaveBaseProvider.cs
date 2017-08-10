namespace ProgramProvidePatternDemo
{
    using System.Configuration;

    public abstract class LogSaveBaseProvider : ILogSaveProvider
    {
        public bool SaveLog(LogEntity logEntity)
        {
            if (!this.IsSaveWithConfiguration(logEntity))
            {
                return false;
            }

            if (!this.ValidateLogEntity(logEntity))
            {
                return false;
            }

            if (!this.DoSave(logEntity))
            {
                return false;
            }

            return true;
        }

        protected abstract bool DoSave(LogEntity logEntity);

        protected virtual void FormatLogContent(LogEntity logEntity)
        {
        }

        protected virtual bool IsSaveWithConfiguration(LogEntity logEntitiy)
        {
            var result = false;
            var type = ConfigurationManager.AppSettings["LogType"];
            if (logEntitiy.Type.ToString() == type)
            {
                result = true;
            }

            return result;
        }

        protected virtual bool ValidateLogEntity(LogEntity logEntity)
        {
            var result = false;
            if (logEntity != null && logEntity.Content != null)
            {
                result = true;
            }

            return result;
        }
    }
}