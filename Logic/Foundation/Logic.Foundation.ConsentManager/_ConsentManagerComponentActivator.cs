using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Bootstrapping;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.Data.GenericDataStoring.Contract;
using Fateblade.Components.Logic.Foundation.ConsentManager.Contract;
using Fateblade.Components.Logic.Foundation.ConsentManager.DataClasses;

namespace Fateblade.Components.Logic.Foundation.ConsentManager
{
    public class ConsentManagerComponentActivator : IComponentActivator
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
            kernel.RegisterUnique<IConsentManager, ConsentManager>(
                new ConsentManager(
                    kernel.Get<IGenericRepository<ConsentEntry>>(), 
                    kernel.Get<IEventBroker>()));
        }

        public void AddMessageSubscriptions(IEventBroker broker)
        {
        }

        public void Configure(IConfigurator config)
        {
        }
    }
}
