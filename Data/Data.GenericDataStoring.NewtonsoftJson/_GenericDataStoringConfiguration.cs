﻿using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration.DataClasses;

namespace Fateblade.Components.Data.GenericDataStoring.NewtonsoftJson
{
    public class GenericDataStoringConfiguration
    {
        [ConfigMap("DataStoring.Generic.Json", "RootPath")]
        public virtual string RootDirectoryPath { get; set; }
    }
}
