using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Bootstrapping;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.Data.GenericDataStoring.Contract;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Components.Data.GenericDataStoring.MariaDb.Tests")]

namespace Fateblade.Components.Data.GenericDataStoring.MariaDb
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>This implementation can only be used for a singular type, and will not be able to support migrating over version changes as it uses efcore with ensure created</remarks>
    public class GenericDataMariaDbStoringActivator : IComponentActivator
    {
        public void Activated()
        {
            throw new NotImplementedException();
        }

        public void Activating()
        {
            throw new NotImplementedException();
        }

        public void AddMessageSubscriptions(IEventBroker broker)
        {
            throw new NotImplementedException();
        }

        public void Configure(IConfigurator config)
        {
            throw new NotImplementedException();
        }

        public void Deactivated()
        {
            throw new NotImplementedException();
        }

        public void Deactivating()
        {
            throw new NotImplementedException();
        }

        public void RegisterMappings(ICoCoKernel kernel)
        {
            kernel.Register(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            kernel.RegisterConfiguration<GenericDataMariaDbStoringConfiguration>();
        }
    }
}
