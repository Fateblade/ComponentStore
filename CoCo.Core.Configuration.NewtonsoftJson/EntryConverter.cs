using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration.DataClasses;

namespace Fateblade.Components.CrossCutting.CoCo.Core.Configuration.NewtonsoftJson
{
    class ConfigEntryConverter
    {
        internal ConfigEntry ConvertToConfigEntry(SerializableConfigEntry serializableConfigEntry, IConfigurationRepository serializingRepository)
        {
            var element = new ConfigEntry()
            {
                Category = serializableConfigEntry.Category,
                Key = serializableConfigEntry.Key,
                Persist = serializableConfigEntry.Persist,
                Source = serializingRepository,
                Value = serializableConfigEntry.Value
            };

            if (serializableConfigEntry.Value.GetType() != serializableConfigEntry.ValueType)
            {
                element.Value = JsonDeserializer.ToObject(element.Value, serializableConfigEntry.ValueType);
            }
            return element;
        }

        internal SerializableConfigEntry ConvertToSerializableConfigEntry(ConfigEntry configEntry)
        {
            return new SerializableConfigEntry()
            {
                Category = configEntry.Category,
                Key = configEntry.Key,
                Persist = configEntry.Persist,
                Value = configEntry.Value,
                ValueType = configEntry.Value.GetType()
            };
        }

        internal List<ConfigEntry> ConvertToConfigEntries(IEnumerable<SerializableConfigEntry> serializableConfigEntries,
            IConfigurationRepository serializingRepository)
        {
            if (serializableConfigEntries == null) { throw new ArgumentException("Cannot convert null reference", new NullReferenceException(nameof(serializableConfigEntries)));}

            var enumeratedSerializableConfigEntries = serializableConfigEntries as SerializableConfigEntry[] ?? serializableConfigEntries.ToArray();
            var list = new List<ConfigEntry>(enumeratedSerializableConfigEntries.Count());
            foreach (var serializableConfigEntry in enumeratedSerializableConfigEntries)
            {
                list.Add(ConvertToConfigEntry(serializableConfigEntry, serializingRepository));
            }

            return list;
        }

        internal List<SerializableConfigEntry> ConvertToSerializableConfigEntries(IEnumerable<ConfigEntry> configEntries)
        {
            if (configEntries == null) { throw new ArgumentException("Cannot convert null reference", new NullReferenceException(nameof(configEntries))); }

            var enumeratedConfigEntries = configEntries as ConfigEntry[] ?? configEntries.ToArray();
            var list = new List<SerializableConfigEntry>(enumeratedConfigEntries.Count());
            foreach (var configEntry in enumeratedConfigEntries)
            {
                list.Add(ConvertToSerializableConfigEntry(configEntry));
            }

            return list;
        }
    }
}
