using Fateblade.Components.CrossCutting.Logging.Contract;
using Fateblade.Components.CrossCutting.Logging.Contract.DataClasses;
using Prism.Logging;

namespace Fateblade.Components.CrossCutting.Logging.PrismWrapper
{
    internal class PrismLogger : ILoggerFacade
    {
        private readonly ILogger _loggerToUse;

        public PrismLogger(ILogger loggerToUse)
        {
            _loggerToUse = loggerToUse;
        }

        public void Log(string message, Category facadeCategory, Priority facadePriority)
        {
            LoggingPriority fPriority = facadePriority == Priority.High ? LoggingPriority.High :
                facadePriority == Priority.Medium ? LoggingPriority.Medium :
                facadePriority == Priority.Low ? LoggingPriority.Low :
                LoggingPriority.None;

            LoggingType type = facadeCategory == Category.Exception ? LoggingType.Exception :
                facadeCategory == Category.Warn ? LoggingType.Warning :
                facadeCategory == Category.Debug ? LoggingType.Debug :
                LoggingType.Information;

            _loggerToUse.Log(LoggingPriority.High, LoggingType.Debug, message);
        }
    }
}
