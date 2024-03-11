using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration.DataClasses;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime
{
    public class CustomDateTimeConfiguration
    {
        [ConfigMap("CustomDateTime", "Sample")]
        public virtual string Sample { get; set; }
    }
}
