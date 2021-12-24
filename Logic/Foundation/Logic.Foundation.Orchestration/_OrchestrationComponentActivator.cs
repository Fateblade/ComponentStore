using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Bootstrapping;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.Logic.Foundation.Orchestration.Contract;

namespace Fateblade.Components.Logic.Foundation.Orchestration
{
    public class OrchestrationComponentActivator : IComponentActivator
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
            Orchestrator instance = new Orchestrator();
            kernel.RegisterUnique<IOrchestrator, Orchestrator>(instance);
        }

        public void AddMessageSubscriptions(IEventBroker broker)
        {
        }

        public void Configure(IConfigurator config)
        {
        }
    }
}
