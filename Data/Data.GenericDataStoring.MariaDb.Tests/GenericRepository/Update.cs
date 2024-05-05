using FluentAssertions;

namespace Fateblade.Components.Data.GenericDataStoring.MariaDb.Tests.GenericRepository
{
    public partial class StandardGuidGenericRepositoryTests
    {
        [Test]
        public void Update_Null_ThrowsException()
        {
            Sut.Invoking((sut) => sut.Update(null)).Should().Throw<Exception>();
        }

        [Test]
        public void Update_EmptyElementChangeValues_GetReturnsNewValues()
        {
            var elementWithOriginalValues = new GuidTestDataClass();
            Sut.Add(elementWithOriginalValues);


            var elementWithUpdatedValues = new GuidTestDataClass()
            {
                Id = elementWithOriginalValues.Id,
                CharValue = 'A',
                DecimalValue = 0.42m,
                DoubleValue = 0.42d,
                EnumValue = EnumTestDataClass.Value2,
                FloatValue = 0.42f,
                StringValue = "Some text"
            };
            Sut.Update(elementWithUpdatedValues);
            
            
            var foundAddedElement = Sut.Query.First(t => t.Id == elementWithUpdatedValues.Id);
            foundAddedElement.Id.Should().Be(elementWithUpdatedValues.Id);
            foundAddedElement.CharValue.Should().Be(elementWithUpdatedValues.CharValue);
            foundAddedElement.DecimalValue.Should().Be(elementWithUpdatedValues.DecimalValue);
            foundAddedElement.DoubleValue.Should().Be(elementWithUpdatedValues.DoubleValue);
            foundAddedElement.FloatValue.Should().Be(elementWithUpdatedValues.FloatValue);
            foundAddedElement.StringValue.Should().Be(elementWithUpdatedValues.StringValue);
        }

        [Test]
        public void Update_ElementWithValuesSetElementsToEmpty_ReturnsElementWithEmptyValues()
        {
            var elementWithOriginalValues = new GuidTestDataClass
            {
                CharValue = 'A',
                DecimalValue = 0.42m,
                DoubleValue = 0.42d,
                EnumValue = EnumTestDataClass.Value2,
                FloatValue = 0.42f,
                StringValue = "Some text"
            };
            Sut.Add(elementWithOriginalValues);


            var elementWithUpdatedValues = new GuidTestDataClass { Id = elementWithOriginalValues.Id };
            Sut.Update(elementWithUpdatedValues);
            
            
            var foundAddedElement = Sut.Query.First(t => t.Id == elementWithUpdatedValues.Id);
            foundAddedElement.Id.Should().Be(elementWithUpdatedValues.Id);
            foundAddedElement.CharValue.Should().Be(elementWithUpdatedValues.CharValue);
            foundAddedElement.DecimalValue.Should().Be(elementWithUpdatedValues.DecimalValue);
            foundAddedElement.DoubleValue.Should().Be(elementWithUpdatedValues.DoubleValue);
            foundAddedElement.FloatValue.Should().Be(elementWithUpdatedValues.FloatValue);
            foundAddedElement.StringValue.Should().Be(elementWithUpdatedValues.StringValue);
        }
    }
}
