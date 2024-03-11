using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Aspects;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.Exceptions;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Contract
{
    [MapException(typeof(CustomDateTimeException))]
    public interface ITimeMachineFactory
    {
        ITimeMachine Create(params DateTimeUnit[] units);
    }
}