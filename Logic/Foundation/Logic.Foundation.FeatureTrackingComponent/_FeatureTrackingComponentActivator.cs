using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Bootstrapping;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.Data.GenericDataStoring.Contract;
using Fateblade.Components.Logic.Foundation.FeatureTrackingComponent.Contract;
using Fateblade.Components.Logic.Foundation.FeatureTrackingComponent.Contract.Messages;
using Fateblade.Components.Logic.Foundation.FeatureTrackingComponent.DataContract;

namespace Fateblade.Components.Logic.Foundation.FeatureTrackingComponent
{
    public class FeatureTrackingComponentActivator : IComponentActivator
    {
        private RegisterFeatureUsageMessageSink _messageSink;

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
            var trackerInstance = new ManualFeatureTracker(kernel.Get<IGenericRepository<IdentifiableFeatureCallAmountEntry>>());

            kernel.RegisterUnique<IFeatureTracker, ManualFeatureTracker>(trackerInstance);
            _messageSink = new RegisterFeatureUsageMessageSink(trackerInstance);
        }

        public void AddMessageSubscriptions(IEventBroker broker)
        {
            broker.Subscribe<RegisterFeatureUsageMessage>(_messageSink.HandleMessage);
        }

        public void Configure(IConfigurator config)
        {
            
        }
    }
}
