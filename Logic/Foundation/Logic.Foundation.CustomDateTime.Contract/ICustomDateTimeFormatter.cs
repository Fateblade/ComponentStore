using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Aspects;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Contract
{
    [MapException(typeof(CustomDateTimeException))]
    public interface ICustomDateTimeFormatter
    {
        string FormatCurrentTime(ITimeMachine timeMachine, string format);
        string Format(DateTimeStamp timeStamp, string format, params DateTimeUnit[] units);
    }
}