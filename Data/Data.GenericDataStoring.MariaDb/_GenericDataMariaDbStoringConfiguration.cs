using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration.DataClasses;

namespace Fateblade.Components.Data.GenericDataStoring.MariaDb
{
    public class GenericDataMariaDbStoringConfiguration
    {
        [ConfigMap("DataStoring.Generic.MariaDb", "DbServerName")]
        public virtual string DbServerName { get; set; }

        [ConfigMap("DataStoring.Generic.MariaDb", "DbName")]
        public virtual string DbName { get; set; }

        [ConfigMap("DataStoring.Generic.MariaDb", "UserName")]
        public virtual string UserName { get; set; }

        [ConfigMap("DataStoring.Generic.MariaDb", "Password")]
#warning TODO: Password as clear string in configs should not be used for production builds!
        public virtual string Password { get; set; }
    }
}
