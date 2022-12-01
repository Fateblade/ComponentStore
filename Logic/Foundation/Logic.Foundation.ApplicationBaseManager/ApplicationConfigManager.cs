using Fateblade.Components.CrossCutting.ApplicationBase;
using Fateblade.Components.CrossCutting.Logging.Contract;
using Fateblade.Components.Data.ApplicationBase.DataStoring.Contract;
using Fateblade.Components.Logic.Foundation.ApplicationBaseManager.Contract;
using System;

namespace Fateblade.Components.Logic.Foundation.ApplicationBaseManager
{
    public class ApplicationConfigManager : IApplicationConfigManager
    {
        private readonly ILogger _logger;
        private readonly IApplicationConfigRepository _repository;
        private ApplicationConfig _config;

        public ApplicationConfigManager(ILogger logger, IApplicationConfigRepository repository)
        {
            _logger = logger;
            _repository = repository;

            ReloadApplicationConfig();
        }

        public bool HasEntry(string key)
        {
            return _config.ConfiguredElements.ContainsKey(key);
        }

        public void SetEntry(string key, ConfigElement entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));

            _config.ConfiguredElements[key] = entry;
        }

        public ConfigElement GetEntry(string key)
        {
            if (!HasEntry(key))
            {
                throw new ArgumentException($"Config does not contain key '{key}'", nameof(key));
            }

            return _config.ConfiguredElements[key];
        }
        
        public ConfigElement GetOrCreateEntry(string key)
        {
            if (!HasEntry(key))
            {
                _config.ConfiguredElements[key] = new ConfigElement();
            }

            return _config.ConfiguredElements[key];
        }

        public void DeleteEntry(string key)
        {
            if (HasEntry(key))
            {
                _config.ConfiguredElements.Remove(key);
            }
        }

        public void SaveApplicationConfig()
        {
            _repository.Save(_config);
        }

        public void ReloadApplicationConfig()
        {
            _config = _repository.Get();
        }
    }
}
