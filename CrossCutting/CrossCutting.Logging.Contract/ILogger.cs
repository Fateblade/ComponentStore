using Fateblade.Components.CrossCutting.Logging.Contract.DataClasses;

namespace Fateblade.Components.CrossCutting.Logging.Contract
{
    public interface ILogger
    {
        void Log(LoggingPriority priority, LoggingType type, string message);
    }
}
