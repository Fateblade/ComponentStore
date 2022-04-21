using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Aspects;
using Fateblade.Components.CrossCutting.ApplicationBase;
using Fateblade.Components.Data.ApplicationBase.DataStoring.Contract.Exceptions;

namespace Fateblade.Components.Data.ApplicationBase.DataStoring.Contract
{
    [MapException(typeof(ApplicationBaseDataStoringException))]
    public interface IApplicationConfigRepository
    {
        ApplicationConfig Get();
        void Save(ApplicationConfig config);
    }
}
