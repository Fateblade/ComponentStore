using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Bootstrapping;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.UI.WPF.ViewModelMapper.Contract;

namespace Fateblade.Components.UI.WPF.ViewModelMapper
{
    public class ViewModelMapperComponentActivator :IComponentActivator
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
            ViewModelDataTemplateMapper instance = new ViewModelDataTemplateMapper();
            kernel.RegisterUnique<IViewModelDataTemplateMapper, ViewModelDataTemplateMapper>(instance);

            ViewModelResourceKeyMapper resourceKeyMapper = new ViewModelResourceKeyMapper();
            kernel.RegisterUnique<IViewModelResourceKeyMapper, ViewModelResourceKeyMapper>(resourceKeyMapper);
        }

        public void AddMessageSubscriptions(IEventBroker broker)
        {
        }

        public void Configure(IConfigurator config)
        {
        }
    }
}
