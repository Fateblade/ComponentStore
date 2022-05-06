using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration.DataClasses;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fateblade.Components.CrossCutting.CoCo.Core.Configuration.NewtonsoftJson
{
    public class DatabaseConfigurationRepository : IConfigurationRepository
    {
        //members
        private readonly string _pathToConfig;
        private List<ConfigEntry> _persistentEntries;
        private List<ConfigEntry> _nonPersistentEntries;
        private readonly JsonSerializerSettings _newtonsoftSettings;
        private readonly ConfigEntryConverter _configEntryConverter;

        //ctors
        public DatabaseConfigurationRepository(string pathToConfig)
        {
            _pathToConfig = pathToConfig;
            _configEntryConverter = new ConfigEntryConverter();
            _newtonsoftSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                CheckAdditionalContent = true,
            };

            loadFromFile();
        }



        //public methods
        public IEnumerable<ConfigEntry> Load()
        {
            if (_nonPersistentEntries == null)
            {
                loadFromFile();
            }

            return _nonPersistentEntries.ToList();
        }

        public void Save(IEnumerable<ConfigEntry> entriesToStore)
        {
            bool persistantChangesMade = false;
            foreach (var entryToStore in entriesToStore)
            {
                saveEntry(entryToStore);
                persistantChangesMade = entryToStore.Persist || persistantChangesMade;
            }

            if (persistantChangesMade)
            {
                savePersistantEntriesToConfigFile();
            }
        }

        public void SaveEntry(ConfigEntry entry)
        {
            saveEntry(entry);
            
            if (entry.Persist)
            {
                savePersistantEntriesToConfigFile();
            }
        }



        //private methods
        private void loadFromFile()
        {
            using (Stream fileStream = File.Open(_pathToConfig, FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fileStream))
                {
                    var fileContent = sr.ReadToEnd();
                    
                    var deserializedObject =
                        JsonConvert.DeserializeObject<List<SerializableConfigEntry>>(fileContent, _newtonsoftSettings);

                    _persistentEntries =
                        _configEntryConverter.ConvertToConfigEntries(
                            deserializedObject ?? new List<SerializableConfigEntry>(), this);

                    _nonPersistentEntries = _persistentEntries?.ToList() ?? new List<ConfigEntry>();
                }
            }
        }

        private void savePersistantEntriesToConfigFile()
        {
            var serializableEntries = _configEntryConverter.ConvertToSerializableConfigEntries(_persistentEntries);

            using (Stream fileStream = File.Open(_pathToConfig, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fileStream))
                {
                    var jsonConfig = JsonConvert.SerializeObject(serializableEntries, _newtonsoftSettings);
                    sw.Write(jsonConfig);
                }
            }
        }

        private void saveEntry(ConfigEntry entryToStore)
        {
            if (_nonPersistentEntries.Any(existingEntry => existingEntry.Category == entryToStore.Category && existingEntry.Key == entryToStore.Key))
            {
                _nonPersistentEntries[_persistentEntries.FindIndex(existingEntry => existingEntry.Category == entryToStore.Category && existingEntry.Key == entryToStore.Key)].Value =
                    entryToStore.Value;
            }
            else
            {
                _nonPersistentEntries.Add(entryToStore);
            }

            if (entryToStore.Persist)
            {
                if (_persistentEntries.Any(existingEntry => existingEntry.Category == entryToStore.Category && existingEntry.Key == entryToStore.Key))
                {
                    _persistentEntries[_persistentEntries.FindIndex(existingEntry => existingEntry.Category == entryToStore.Category && existingEntry.Key == entryToStore.Key)].Value =
                        entryToStore.Value;
                }
                else
                {
                    _persistentEntries.Add(entryToStore);
                }
            }
        }
    }
}
