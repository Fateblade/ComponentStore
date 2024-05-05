using FluentAssertions;

namespace Fateblade.Components.Data.GenericDataStoring.MariaDb.Tests.CrossTables
{
    public partial class ForeignKeyGuidGenericRepositoryTests
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
            var emptyElement = new GuidCrossTestDataClass();
            Sut.Add(emptyElement);


            var foundAddedElement = Sut.Query.First(t => t.Id == emptyElement.Id);


            foundAddedElement.Id.Should().Be(emptyElement.Id);
            foundAddedElement.CrossValue.Should().BeSameAs(emptyElement.CrossValue);
        }

        [Test]
        public void Query_AddedElementWithValues_ReturnsElementWithCorrectValues()
        {
            var foreignElement = new GuidTestDataClass
            {
                CharValue = 'A',
                DecimalValue = 0.42m,
                DoubleValue = 0.42d,
                EnumValue = EnumTestDataClass.Value2,
                FloatValue = 0.42f,
                StringValue = "Some text"
            };
            var foreignElement2 = new GuidTestDataClass
            {
                CharValue = 'A',
                DecimalValue = 0.42m,
                DoubleValue = 0.42d,
                EnumValue = EnumTestDataClass.Value2,
                FloatValue = 0.42f,
                StringValue = "Some text"
            };
            var elementWithValues = new GuidCrossTestDataClass
            {
                CrossValue = [foreignElement, foreignElement2]
            };
            Sut.Add(elementWithValues);


            var foundAddedElement = Sut.Query.First(t => t.Id == elementWithValues.Id);


            foundAddedElement.Id.Should().Be(elementWithValues.Id);
            foundAddedElement.CrossValue.Should().BeSameAs(elementWithValues.CrossValue);
            foundAddedElement.CrossValue[0].CharValue.Should().Be(elementWithValues.CrossValue[0].CharValue);
            foundAddedElement.CrossValue[0].DecimalValue.Should().Be(elementWithValues.CrossValue[0].DecimalValue);
            foundAddedElement.CrossValue[0].DoubleValue.Should().Be(elementWithValues.CrossValue[0].DoubleValue);
            foundAddedElement.CrossValue[0].FloatValue.Should().Be(elementWithValues.CrossValue[0].FloatValue);
            foundAddedElement.CrossValue[0].StringValue.Should().Be(elementWithValues.CrossValue[0].StringValue);
            foundAddedElement.CrossValue[1].CharValue.Should().Be(elementWithValues.CrossValue[1].CharValue);
            foundAddedElement.CrossValue[1].DecimalValue.Should().Be(elementWithValues.CrossValue[1].DecimalValue);
            foundAddedElement.CrossValue[1].DoubleValue.Should().Be(elementWithValues.CrossValue[1].DoubleValue);
            foundAddedElement.CrossValue[1].FloatValue.Should().Be(elementWithValues.CrossValue[1].FloatValue);
            foundAddedElement.CrossValue[1].StringValue.Should().Be(elementWithValues.CrossValue[1].StringValue);
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(8)]
        [TestCase(16)]
        public void Query_MultipleAddedElements_ReturnsExpectedNumberOfElements(int numberOfElements)
        {
            var elements = new List<GuidCrossTestDataClass>(numberOfElements);
            for (var i = 0; i < numberOfElements; i++)
            {
                var element = new GuidCrossTestDataClass();
                elements.Add(element);
            }
            Sut.AddRange(elements);


            var foundElements = Sut.Query.Where(t=> elements.Select(x=>x.Id).Contains(t.Id));


            foundElements.Count().Should().Be(numberOfElements);
        }
    }
}
