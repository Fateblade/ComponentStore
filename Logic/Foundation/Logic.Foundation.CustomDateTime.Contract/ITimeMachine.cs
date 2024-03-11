using System.Collections.Generic;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Aspects;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Contract
{
    [MapException(typeof(CustomDateTimeException))]
    public interface ITimeMachine
    {
        IReadOnlyCollection<DateTimeUnit> TimeFormat { get; }
        DateTimeStamp CurrentTime { get; }

        /// <summary>
        /// Moves the CurrentTime of the time machine forward by the specified amount
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="amount"></param>
        /// <returns>the new current time value</returns>
        DateTimeStamp MoveForward(DateTimeUnit unit, int amount);

        /// <summary>
        /// Moves the CurrentTime of the time machine backward by the specified amount
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="amount"></param>
        /// <returns>the new current time value</returns>
        DateTimeStamp MoveBackward(DateTimeUnit unit, int amount);

        /// <summary>
        /// Sets the current time to the specified time
        /// </summary>
        /// <param name="timeStamp"></param>
        void SetTime(DateTimeStamp timeStamp);
    }
}