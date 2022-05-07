using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Bootstrapping;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.CrossCutting.Logging.Contract;
using Fateblade.Components.Data.ApplicationBase.DataStoring.Contract;
using Fateblade.Components.Logic.Foundation.ApplicationBaseManager.Contract;

namespace Fateblade.Components.Logic.Foundation.ApplicationBaseManager
{
    public class ApplicationBaseManagerComponentActivator : IComponentActivator
    {
        public void Activating()
        {
        }

        public void Activated()
        {
        }

        public void Deactivating()
        {
        }

        public void Deactivated()
        {
        }

        public void RegisterMappings(ICoCoKernel kernel)
        {
            kernel.RegisterUnique(typeof(IApplicationConfigManager), new ApplicationConfigManager(kernel.Get<ILogger>(), kernel.Get<IApplicationConfigRepository>()));
        }

        public void AddMessageSubscriptions(IEventBroker broker)
        {
        }

        public void Configure(IConfigurator config)
        {
        }
    }
}
