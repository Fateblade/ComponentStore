using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Bootstrapping;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime
{
    public class CustomDateTimeComponentActivator : IComponentActivator
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
            kernel.Register<ITimeMachineFactory, TimeMachineFactory>();
            kernel.Register<ITimeMachine, TimeMachine>();
            kernel.Register<ICustomDateTimeFormatter, CustomDateTimeFormatter>();
        }

        public void AddMessageSubscriptions(IEventBroker broker)
        {

        }

        public void Configure(IConfigurator config)
        {

        }
    }
}
