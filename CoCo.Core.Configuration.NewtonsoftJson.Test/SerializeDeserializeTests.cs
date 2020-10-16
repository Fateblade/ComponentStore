using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Configuration.DataClasses;
using Fateblade.Components.CrossCutting.CoCo.Core.Configuration.NewtonsoftJson;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoCo.Core.Configuration.NewtonsoftJson.Test
{
    [TestClass]
    public class SerializeDeserializeTests
    {
        private List<ConfigEntry> _testDataConfigEntries;
        private ConfigEntry _singleConfigEntry;
        private string _filePath;
        private DatabaseConfigurationRepository _serializeTarget;
        private DatabaseConfigurationRepository _deserializeTarget;


        [TestInitialize]
        public void Initialize()
        {
            _testDataConfigEntries = new List<ConfigEntry>();
            _testDataConfigEntries.Add(new ConfigEntry(){Key = "Int", Category="TestData", Persist= true, Value = 5});
            _testDataConfigEntries.Add(new ConfigEntry() { Key = "Double", Category = "TestData", Persist = true, Value = 5.23d });
            _testDataConfigEntries.Add(new ConfigEntry() { Key = "String", Category = "TestData", Persist = true, Value = "Some data" });

            _singleConfigEntry = new ConfigEntry() {Key = "SingleEntry", Category = "TestData", Persist = true, Value = "Some data"};

            _filePath = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString());
            
        }

        [TestMethod]
        public void SimpleSerializeDeserializeTest()
        {
            _serializeTarget = new DatabaseConfigurationRepository(_filePath);
            _serializeTarget.Save(_testDataConfigEntries);
            _serializeTarget.SaveEntry(_singleConfigEntry);


            _deserializeTarget = new DatabaseConfigurationRepository(_filePath);
            var deserializedConfigEntries = _deserializeTarget.Load().ToList();


            Assert.AreEqual(_testDataConfigEntries.Count+1, deserializedConfigEntries.Count());

            Assert.IsTrue(deserializedConfigEntries.Count(t => !_testDataConfigEntries.Any(x => x.Category == t.Category && x.Key == t.Key)) == 1);
            Assert.IsTrue(deserializedConfigEntries.Count(x => x.Category == _singleConfigEntry.Category && x.Key == _singleConfigEntry.Key) == 1);

        }

        [TestMethod]
        public void ComplexSerializeDeserializeTest()
        {//still needs work for complex types with object in them... maybe a conversion into more info, alternative write own jsonconverter instead of doing postfixes?
            _serializeTarget = new DatabaseConfigurationRepository(_filePath);
            var complexEntry = new ConfigEntry()
            {
                Category = "blub",
                Key="blab",
                Persist = true,
                Value = new Dictionary<string, Dictionary<int, object>>()
                {
                    { "a", new Dictionary<int, object>(){ {1,5}, { 2,"string"}, { 3, _singleConfigEntry} } },
                    { "b", new Dictionary<int, object>(){ {1,5}, { 2,"string"}, { 3, _singleConfigEntry} } },
                    { "c", new Dictionary<int, object>(){ {1,5}, { 2,"string"}, { 3, _singleConfigEntry} } }
                }
            };

            _serializeTarget.SaveEntry(complexEntry);


            _deserializeTarget = new DatabaseConfigurationRepository(_filePath);
            var deserializedConfigEntries = _deserializeTarget.Load().ToList();

            Assert.IsFalse(deserializedConfigEntries.Any(t => t.Value == null));
        }
    }
}
