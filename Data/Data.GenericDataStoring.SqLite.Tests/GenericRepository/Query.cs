using FluentAssertions;

namespace Fateblade.Components.Data.GenericDataStoring.SqLite.Tests.GenericRepository
{
    public partial class StandardGuidGenericRepositoryTests
    {

        [Test]
        public void Query_WrongId_DoesNotReturnElement()
        {
            var randomId = Guid.NewGuid();
            var foundAddedElement = Sut.Query.FirstOrDefault(t => t.Id == randomId);


            foundAddedElement.Should().BeNull();
        }

        [Test]
        public void Query_AddedEmptyElement_ReturnsEmptyElement()
        {
            var emptyElement = new GuidTestDataClass();
            Sut.Add(emptyElement);


            var foundAddedElement = Sut.Query.First(t => t.Id == emptyElement.Id);


            foundAddedElement.Id.Should().Be(emptyElement.Id);
            foundAddedElement.CharValue.Should().Be(emptyElement.CharValue);
            foundAddedElement.DecimalValue.Should().Be(emptyElement.DecimalValue);
            foundAddedElement.DoubleValue.Should().Be(emptyElement.DoubleValue);
            foundAddedElement.FloatValue.Should().Be(emptyElement.FloatValue);
            foundAddedElement.StringValue.Should().Be(emptyElement.StringValue);
        }

        [Test]
        public void Query_AddedElementWithValues_ReturnsElementWithCorrectValues()
        {
            var elementWithValues = new GuidTestDataClass
            {
                CharValue = 'A',
                DecimalValue = 0.42m,
                DoubleValue = 0.42d,
                EnumValue = EnumTestDataClass.Value2,
                FloatValue = 0.42f,
                StringValue = "Some text"
            };
            Sut.Add(elementWithValues);


            var foundAddedElement = Sut.Query.First(t => t.Id == elementWithValues.Id);


            foundAddedElement.Id.Should().Be(elementWithValues.Id);
            foundAddedElement.CharValue.Should().Be(elementWithValues.CharValue);
            foundAddedElement.DecimalValue.Should().Be(elementWithValues.DecimalValue);
            foundAddedElement.DoubleValue.Should().Be(elementWithValues.DoubleValue);
            foundAddedElement.FloatValue.Should().Be(elementWithValues.FloatValue);
            foundAddedElement.StringValue.Should().Be(elementWithValues.StringValue);
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(8)]
        [TestCase(16)]
        public void Query_MultipleAddedElements_ReturnsExpectedNumberOfElements(int numberOfElements)
        {
            var elements = new List<GuidTestDataClass>(numberOfElements);
            for (var i = 0; i < numberOfElements; i++)
            {
                var element = new GuidTestDataClass();
                elements.Add(element);
            }
            Sut.AddRange(elements);


            var foundElements = Sut.Query.Where(t=> elements.Select(x=>x.Id).Contains(t.Id));


            foundElements.Count().Should().Be(numberOfElements);
        }
    }
}
