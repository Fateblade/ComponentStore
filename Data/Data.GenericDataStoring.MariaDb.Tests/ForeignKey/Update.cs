using FluentAssertions;

namespace Fateblade.Components.Data.GenericDataStoring.MariaDb.Tests.ForeignKey
{
    public partial class ForeignKeyGuidGenericRepositoryTests
    {
        [Test]
        public void Update_Null_ThrowsException()
        {
            Sut.Invoking((sut) => sut.Update(null)).Should().Throw<Exception>();
        }

        [Test]
        public void Update_EmptyElementChangeValues_GetReturnsNewValues()
        {
            var elementWithOriginalValues = new GuidForeignTestDataClass();
            Sut.Add(elementWithOriginalValues);


            var foreignElement = new GuidTestDataClass
            {
                CharValue = 'A',
                DecimalValue = 0.42m,
                DoubleValue = 0.42d,
                EnumValue = EnumTestDataClass.Value2,
                FloatValue = 0.42f,
                StringValue = "Some text"
            };
            var elementWithUpdatedValues = new GuidForeignTestDataClass
            {
                Id = elementWithOriginalValues.Id,
                ForeignValue = foreignElement
            };
            Sut.Update(elementWithUpdatedValues);
            
            
            var foundAddedElement = Sut.Query.First(t => t.Id == elementWithUpdatedValues.Id);
            foundAddedElement.Id.Should().Be(elementWithUpdatedValues.Id);
            foundAddedElement.ForeignValue.Should().Be(elementWithUpdatedValues.ForeignValue);
            foundAddedElement.ForeignValue.CharValue.Should().Be(elementWithUpdatedValues.ForeignValue.CharValue);
            foundAddedElement.ForeignValue.DecimalValue.Should().Be(elementWithUpdatedValues.ForeignValue.DecimalValue);
            foundAddedElement.ForeignValue.DoubleValue.Should().Be(elementWithUpdatedValues.ForeignValue.DoubleValue);
            foundAddedElement.ForeignValue.FloatValue.Should().Be(elementWithUpdatedValues.ForeignValue.FloatValue);
            foundAddedElement.ForeignValue.StringValue.Should().Be(elementWithUpdatedValues.ForeignValue.StringValue);
        }

        [Test]
        public void Update_ElementWithValuesSetElementsToEmpty_ReturnsElementWithEmptyValues()
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
            var elementWithOriginalValues = new GuidForeignTestDataClass
            {
                ForeignValue = foreignElement
            };
            Sut.Add(elementWithOriginalValues);


            var elementWithUpdatedValues = new GuidForeignTestDataClass { Id = elementWithOriginalValues.Id };
            Sut.Update(elementWithUpdatedValues);
            
            
            var foundAddedElement = Sut.Query.First(t => t.Id == elementWithUpdatedValues.Id);
            foundAddedElement.Id.Should().Be(elementWithUpdatedValues.Id); 
            foundAddedElement.ForeignValue.Should().Be(elementWithUpdatedValues.ForeignValue);
        }
    }
}
