using System.Runtime.CompilerServices;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Bootstrapping;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.Data.ApplicationBase.DataStoring.Contract;

[assembly: InternalsVisibleTo("Components.Data.ApplicationBase.DataStoring.NewtonsoftJson.Tests")]

namespace Fateblade.Components.Data.ApplicationBase.DataStoring.NewtonsoftJson
{
    public class ApplicationBaseDataStoringComponentActivator : IComponentActivator
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
            kernel.Register<IApplicationConfigRepository, ApplicationConfigRepository>();
        }

        public void AddMessageSubscriptions(IEventBroker broker)
        {
        }

        public void Configure(IConfigurator config)
        {
        }
    }
}

