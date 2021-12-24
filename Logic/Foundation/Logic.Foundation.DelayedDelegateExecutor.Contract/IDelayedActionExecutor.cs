using System;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Aspects;
using Fateblade.Components.Logic.Foundation.DelayedActionExecution.Contract.Exceptions;

namespace Fateblade.Components.Logic.Foundation.DelayedActionExecution.Contract
{
    [MapException(typeof(DelayedActionExecutionException))]
    public interface IDelayedActionExecutor
    {
        /// <summary>
        /// delays the execution of the action
        /// </summary>
        /// <param name="actionToExecute">action to execute after a delays</param>
        /// <param name="millisecondsToDelayAtLeast">minimum time the action should be delayed, depending on implementation actual execution may be delayed longer</param>
        void DelayExecution(Action actionToExecute, int millisecondsToDelayAtLeast);
    }
}
