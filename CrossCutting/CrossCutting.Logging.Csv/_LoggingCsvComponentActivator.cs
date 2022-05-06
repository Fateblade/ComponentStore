﻿using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Bootstrapping;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.DependencyInjection.DataClasses;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.CrossCutting.CommonMessages.Contract.Messages;
using Fateblade.Components.CrossCutting.Logging.Contract;

namespace Fateblade.Components.CrossCutting.Logging.Csv
{
    public class LoggingCsvComponentActivator : IComponentActivator
    {
        private CsvLogger _csvLogger;

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
            kernel.RegisterConfiguration<LoggingCsvConfiguration>();
            kernel.Register<ILogger, CsvLogger>(RegisterScope.Unique);
            _csvLogger =(CsvLogger)kernel.Get<ILogger>();
        }

        public void AddMessageSubscriptions(IEventBroker broker)
        {
            broker.Subscribe<ShutdownIssuedMessage>(_csvLogger.HandleShutdownNotize);
        }

        public void Configure(IConfigurator config)
        {
        }
    }
}