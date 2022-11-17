using Fateblade.Components.Logic.Foundation.Translation.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Fateblade.Components.Logic.Foundation.Translation
{
    internal class ResourceFileTranslationStringProvider : ITranslationStringProvider
    {
        private readonly Dictionary<string, Dictionary<string, string>> _knownStringResources;
        private readonly List<ResourceFilePathInfo> _knownResourceFilePaths;
        private string _defaultLanguageKey;

        public ResourceFileTranslationStringProvider()
        {
            _knownStringResources = new Dictionary<string, Dictionary<string, string>>();
            _knownResourceFilePaths = new List<ResourceFilePathInfo>();
            _defaultLanguageKey = "de";
        }

        //public ResourceFileTranslationStringProvider(string defaultLanguageKey) : this()
        //{
        //    _defaultLanguageKey = defaultLanguageKey;
        //}

        public void ChangeDefaultLanguage(string newDefaultLanguageKey)
        {
            _defaultLanguageKey = newDefaultLanguageKey;
        }

        public void LoadStringResourcesForDefaultLanguage(string resourceFileName)
        {
            LoadStringResourcesForLanguage(resourceFileName, _defaultLanguageKey);
        }

        public void LoadStringResourcesForLanguage(string resourceFileName, string languageKeyForContainedStrings)
        {
            var assembly = Assembly.GetCallingAssembly();

            var resourcePath = assembly.GetManifestResourceNames().FirstOrDefault(t => t.Contains(resourceFileName));
            if (string.IsNullOrWhiteSpace(resourcePath)) { throw new ArgumentException("Resource file not found for given name", nameof(resourceFileName)); }

            if (resourcePath.EndsWith(".resources"))
            {
                resourcePath = resourcePath[..^".resources".Length]; //resourcePath = resourcePath.Substring(0, resourcePath.Length - ".resources".Length);
            }

            _knownResourceFilePaths.Add(new ResourceFilePathInfo()
            {
                LanguageKey = languageKeyForContainedStrings,
                ContainingAssembly = assembly,
                FilePath = resourcePath
            });
        }

        public string GetString(string key)
        {
            return GetString(key, _defaultLanguageKey);
        }

        public string GetString(string key, string languageKey)
        {
            if (tryGetKnownString(key, _defaultLanguageKey, out string knownString))
            {
                return knownString;
            }

            return findStringInResourceFiles(key, languageKey);
        }

        private string findStringInResourceFiles(string key, string languageKey)
        {
            foreach (var resourceFilePathInfo in _knownResourceFilePaths.Where(t => t.LanguageKey == languageKey))
            {
                var resourceManager = new ResourceManager(resourceFilePathInfo.FilePath, resourceFilePathInfo.ContainingAssembly);

                var foundString = resourceManager.GetString(key);
                if (foundString != null)
                {
                    addNewKnownString(key, languageKey, foundString);
                    return foundString;
                }
            }

            throw new ArgumentException($"Could not find translated string for '{key}' in language '{languageKey}'");
        }

        private void addNewKnownString(string key, string languageKey, string foundString)
        {
            if (!_knownStringResources.ContainsKey(languageKey))
            {
                _knownStringResources.Add(languageKey, new Dictionary<string, string>());
            }

            _knownStringResources[languageKey].Add(key, foundString);
        }

        private bool tryGetKnownString(string key, string languageKey, out string foundValue)
        {
            foundValue = string.Empty;
            var foundString = false;

            if (_knownStringResources.TryGetValue(languageKey, out Dictionary<string, string> knownStringsOfLanguage))
            {
                foundString = knownStringsOfLanguage.TryGetValue(key, out foundValue);
            }

            return foundString;
        }


        private struct ResourceFilePathInfo
        {
            public string LanguageKey { get; set; }
            public string FilePath { get; set; }
            public Assembly ContainingAssembly { get; set; }
        }
    }
}
