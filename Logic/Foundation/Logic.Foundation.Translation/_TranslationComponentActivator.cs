using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Bootstrapping;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection.DataClasses;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.Logic.Foundation.Translation.Contract;

namespace Fateblade.Components.Logic.Foundation.Translation
{
    public class TranslationComponentActivator : IComponentActivator
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
            kernel.Register<ITranslationStringProvider, ResourceFileTranslationStringProvider>(RegisterScope.Unique);
        }

        public void AddMessageSubscriptions(IEventBroker broker)
        {
        }

        public void Configure(IConfigurator config)
        {

        }
    }
}
