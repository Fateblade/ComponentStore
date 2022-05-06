using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration.DataClasses;

namespace Fateblade.Components.CrossCutting.Logging.Csv
{
    public class LoggingCsvConfiguration
    {
        [ConfigMap("Logging", "MessageBufferCount", true)]
        public virtual int MessageBufferCount { get; set; }

        [ConfigMap("Logging", "FullPathToLogFile", true)]
        public virtual string FullPathToLogFile { get; set; }
    }
}
