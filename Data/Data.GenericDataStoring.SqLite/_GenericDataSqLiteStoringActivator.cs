using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Bootstrapping;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.Data.GenericDataStoring.Contract;
using System;

namespace Fateblade.Components.Data.GenericDataStoring.SqLite
{
    public class GenericDataSqLiteStoringActivator : IComponentActivator
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

            kernel.RegisterConfiguration<GenericDataSqLiteStoringConfiguration>();
        }
    }
}
