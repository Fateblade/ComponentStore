using System.Collections.Generic;
using System.IO;
using Fateblade.Components.CrossCutting.ApplicationBase;
using Fateblade.Components.CrossCutting.Logging.Contract;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Fateblade.Components.Data.ApplicationBase.DataStoring.NewtonsoftJson.Tests
{

    public class ApplicationConfigRepositoryTests
    {
        private readonly ApplicationConfigRepository _target;

        public static TestCaseData[] RoundtripTestCaseData = new []
        {
            new TestCaseData("Empty Config", new ApplicationConfig()),
            new TestCaseData("Single String Configs", new ApplicationConfig
            {
                ConfiguredElements = new Dictionary<string, ConfigElement>
                {
                    {"Test", new ConfigElement(){Value = "String" }}
                }
            }),
            new TestCaseData("Multiple String Configs", new ApplicationConfig
            {
                ConfiguredElements = new Dictionary<string, ConfigElement>
                {
                    {"Test", new ConfigElement(){Value = "String" }},
                    {"Test2", new ConfigElement(){Value = "String2" }}
                }
            })
        };

        public ApplicationConfigRepositoryTests()
        {
            Mock<ILogger> loggerMock = new Mock<ILogger>();

            _target = new ApplicationConfigRepository(loggerMock.Object);
        }

        [SetUp]
        public void ClearFiles()
        {
            if(File.Exists(_target._completePath))
                File.Delete(_target._completePath);
        }

        [Test]
        [TestCaseSource(typeof(ApplicationConfigRepositoryTests), nameof(RoundtripTestCaseData))]
        public void RoundtripSerialization(string name, ApplicationConfig toTest)
        {
            _target.Save(toTest);

            var deserializedFromFile = _target.Get();

            deserializedFromFile.Should().NotBeNull(name);
            deserializedFromFile.ConfiguredElements.Should().NotBeNull(name); 
            deserializedFromFile.ConfiguredElements.Count.Should().Be(toTest.ConfiguredElements.Count, name);

            foreach (var element in toTest.ConfiguredElements)
            {
                deserializedFromFile.ConfiguredElements.ContainsKey(element.Key).Should().BeTrue();
                deserializedFromFile.ConfiguredElements[element.Key].Should().NotBeNull();
                deserializedFromFile.ConfiguredElements[element.Key].Value.Should().NotBeNull();
                deserializedFromFile.ConfiguredElements[element.Key].Value.Should().Be(element.Value.Value);
            }
        }
    }
}
