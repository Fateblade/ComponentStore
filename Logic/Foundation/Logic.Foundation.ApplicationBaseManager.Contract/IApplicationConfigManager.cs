using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Aspects;
using Fateblade.Components.CrossCutting.ApplicationBase;
using Fateblade.Components.Logic.Foundation.ApplicationBaseManager.Contract.Exceptions;

namespace Fateblade.Components.Logic.Foundation.ApplicationBaseManager.Contract
{
    [MapException(typeof(ApplicationBaseManagerException))]
    public interface IApplicationConfigManager
    {
        void SetEntry(string key, ConfigElement entry);
        ConfigElement GetEntry(string key);

        void SaveApplicationConfig();
        void ReloadApplicationConfig();
    }
}
