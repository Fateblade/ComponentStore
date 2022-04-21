using System.Collections.Generic;

namespace Fateblade.Components.CrossCutting.ApplicationBase
{
    public class ApplicationConfig
    {
        public Dictionary<string, ConfigElement> ConfiguredElements { get; set; }

        public ApplicationConfig()
        {
            ConfiguredElements = new Dictionary<string, ConfigElement>();
        }
    }
}
