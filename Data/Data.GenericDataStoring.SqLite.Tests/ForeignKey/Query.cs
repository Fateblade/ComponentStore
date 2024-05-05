using FluentAssertions;

namespace Fateblade.Components.Data.GenericDataStoring.SqLite.Tests.ForeignKey
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
            var emptyElement = new GuidForeignTestDataClass();
            Sut.Add(emptyElement);


            var foundAddedElement = Sut.Query.First(t => t.Id == emptyElement.Id);


            foundAddedElement.Id.Should().Be(emptyElement.Id);
            foundAddedElement.ForeignValue.Should().Be(emptyElement.ForeignValue);
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
            var elementWithValues = new GuidForeignTestDataClass
            {
                ForeignValue = foreignElement
            };
            Sut.Add(elementWithValues);


            var foundAddedElement = Sut.Query.First(t => t.Id == elementWithValues.Id);


            foundAddedElement.Id.Should().Be(elementWithValues.Id);
            foundAddedElement.ForeignValue.Should().Be(elementWithValues.ForeignValue);
            foundAddedElement.ForeignValue.CharValue.Should().Be(elementWithValues.ForeignValue.CharValue);
            foundAddedElement.ForeignValue.DecimalValue.Should().Be(elementWithValues.ForeignValue.DecimalValue);
            foundAddedElement.ForeignValue.DoubleValue.Should().Be(elementWithValues.ForeignValue.DoubleValue);
            foundAddedElement.ForeignValue.FloatValue.Should().Be(elementWithValues.ForeignValue.FloatValue);
            foundAddedElement.ForeignValue.StringValue.Should().Be(elementWithValues.ForeignValue.StringValue);
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(8)]
        [TestCase(16)]
        public void Query_MultipleAddedElements_ReturnsExpectedNumberOfElements(int numberOfElements)
        {
            var elements = new List<GuidForeignTestDataClass>(numberOfElements);
            for (var i = 0; i < numberOfElements; i++)
            {
                var element = new GuidForeignTestDataClass();
                elements.Add(element);
            }
            Sut.AddRange(elements);


            var foundElements = Sut.Query.Where(t=> elements.Select(x=>x.Id).Contains(t.Id));


            foundElements.Count().Should().Be(numberOfElements);
        }
    }
}
