using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration.DataClasses;

namespace Fateblade.Components.Data.GenericDataStoring.SqLite
{
    public class GenericDataSqLiteStoringConfiguration
    {
        [ConfigMap("DataStoring.Generic.SqLite", "DbDirectoryPath")]
        public virtual string DbDirectoryPath { get; set; }

        [ConfigMap("DataStoring.Generic.SqLite", "DbName")]
        public virtual string DbName { get; set; }
    }
}
