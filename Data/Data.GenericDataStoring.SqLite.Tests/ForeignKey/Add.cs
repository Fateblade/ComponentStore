using FluentAssertions;

namespace Fateblade.Components.Data.GenericDataStoring.SqLite.Tests.ForeignKey
{
    public partial class ForeignKeyGuidGenericRepositoryTests
    {
        [Test]
        public void Add_Null_ThrowsException()
        {
            Sut.Invoking((sut)=>sut.Add(null)).Should().Throw<Exception>();
        }

        [Test]
        public void Add_EmptyElement_GetsIdAssigned()
        {
            var elementToSave = new GuidForeignTestDataClass();
            
            Sut.Add(elementToSave);

            elementToSave.Id.Should().NotBe(Guid.Empty);
        }

        [Test]
        public void Add_ElementWithValues_DoesNotThrow()
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
            var elementToSave = new GuidForeignTestDataClass
            {
                ForeignValue = foreignElement
            };

            Sut.Invoking((sut) => sut.Add(elementToSave)).Should().NotThrow();
        }
    }
}
