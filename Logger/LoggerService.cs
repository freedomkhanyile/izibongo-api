using izibongo.api.DAL.Contracts.ILoggerService;
using NLog;
namespace izibongo.api.Logger
{
    public class LoggerService : ILoggerService
    {
        public LoggerService(){}
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
             logger.Error(message);
        }

        public void LogInfo(string message)
        {
              logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}