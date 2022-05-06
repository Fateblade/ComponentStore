using System;

namespace Fateblade.Components.CrossCutting.CoCo.Core.Configuration.NewtonsoftJson
{
    class SerializableConfigEntry
    {
        public string Category { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
        public bool Persist { get; set; }
        public Type ValueType { get; set; }

        public SerializableConfigEntry()
        { }
    }
}
