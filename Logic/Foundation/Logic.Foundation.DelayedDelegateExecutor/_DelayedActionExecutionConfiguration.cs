using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration.DataClasses;

namespace Fateblade.Components.Logic.Foundation.DelayedActionExecution
{
    public class DelayedActionExecutionConfiguration
    {
        [ConfigMap("DelayedActionExecution", "ExecutionCheckDelay")]
        public int ExecutionCheckDelay { get; set; }
    }
}
