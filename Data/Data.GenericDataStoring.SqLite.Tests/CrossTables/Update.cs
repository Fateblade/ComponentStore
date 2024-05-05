using FluentAssertions;

namespace Fateblade.Components.Data.GenericDataStoring.SqLite.Tests.CrossTables
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
            var elementWithOriginalValues = new GuidCrossTestDataClass();
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
            var foreignElement2 = new GuidTestDataClass
            {
                CharValue = 'A',
                DecimalValue = 0.42m,
                DoubleValue = 0.42d,
                EnumValue = EnumTestDataClass.Value2,
                FloatValue = 0.42f,
                StringValue = "Some text"
            };
            var elementWithUpdatedValues = new GuidCrossTestDataClass
            {
                Id = elementWithOriginalValues.Id,
                CrossValue = [foreignElement, foreignElement2]
            };
            Sut.Update(elementWithUpdatedValues);
            
            
            var foundAddedElement = Sut.Query.First(t => t.Id == elementWithUpdatedValues.Id);
            foundAddedElement.Id.Should().Be(elementWithUpdatedValues.Id);
            foundAddedElement.CrossValue.Should().BeEquivalentTo(elementWithUpdatedValues.CrossValue);
            foundAddedElement.CrossValue[0].CharValue.Should().Be(elementWithUpdatedValues.CrossValue[0].CharValue);
            foundAddedElement.CrossValue[0].DecimalValue.Should().Be(elementWithUpdatedValues.CrossValue[0].DecimalValue);
            foundAddedElement.CrossValue[0].DoubleValue.Should().Be(elementWithUpdatedValues.CrossValue[0].DoubleValue);
            foundAddedElement.CrossValue[0].FloatValue.Should().Be(elementWithUpdatedValues.CrossValue[0].FloatValue);
            foundAddedElement.CrossValue[0].StringValue.Should().Be(elementWithUpdatedValues.CrossValue[0].StringValue);
            foundAddedElement.CrossValue[1].CharValue.Should().Be(elementWithUpdatedValues.CrossValue[1].CharValue);
            foundAddedElement.CrossValue[1].DecimalValue.Should().Be(elementWithUpdatedValues.CrossValue[1].DecimalValue);
            foundAddedElement.CrossValue[1].DoubleValue.Should().Be(elementWithUpdatedValues.CrossValue[1].DoubleValue);
            foundAddedElement.CrossValue[1].FloatValue.Should().Be(elementWithUpdatedValues.CrossValue[1].FloatValue);
            foundAddedElement.CrossValue[1].StringValue.Should().Be(elementWithUpdatedValues.CrossValue[1].StringValue);
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
            var foreignElement2 = new GuidTestDataClass
            {
                CharValue = 'A',
                DecimalValue = 0.42m,
                DoubleValue = 0.42d,
                EnumValue = EnumTestDataClass.Value2,
                FloatValue = 0.42f,
                StringValue = "Some text"
            };
            var elementWithOriginalValues = new GuidCrossTestDataClass
            {
                CrossValue = [foreignElement, foreignElement2]
            };
            Sut.Add(elementWithOriginalValues);
            var idToLookUp = elementWithOriginalValues.CrossValue[0].Id;

            var elementWithUpdatedValues = new GuidCrossTestDataClass {
                Id = elementWithOriginalValues.Id, 
                CrossValue = []
            };
            Sut.Update(elementWithUpdatedValues);
            
            
            var foundAddedElement = Sut.Query.First(t => t.Id == elementWithUpdatedValues.Id);
            foundAddedElement.Id.Should().Be(elementWithUpdatedValues.Id); 
            foundAddedElement.CrossValue.Should().BeEquivalentTo(elementWithUpdatedValues.CrossValue);
            GuidTestDataClassRepository.Query.FirstOrDefault(t => t.Id == idToLookUp).Should().NotBeNull();
        }
    }
}
