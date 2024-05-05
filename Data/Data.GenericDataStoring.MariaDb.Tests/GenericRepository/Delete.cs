using FluentAssertions;

namespace Fateblade.Components.Data.GenericDataStoring.MariaDb.Tests.GenericRepository
{
    public partial class StandardGuidGenericRepositoryTests
    {
        [Test]
        public void Delete_Null_ThrowsError()
        {
            Sut.Invoking((sut) => sut.Delete(null)).Should().Throw<Exception>();
        }

        [Test]
        public void Delete_ElementWithNonExistingId_ThrowsErrorChangesNothing()
        {
            var elementToDelete = new GuidTestDataClass(){Id = Guid.NewGuid() };
            var previousExistingElementCount = Sut.Query.Count();


            Sut.Invoking((sut) => sut.Delete(elementToDelete)).Should().Throw<Exception>();


            Sut.Query.Count().Should().Be(previousExistingElementCount);
        }

        [Test]
        public void Delete_ExistingElement_ElementIsDeleted()
        {
            var element = new GuidTestDataClass
            {
                CharValue = 'A',
                DecimalValue = 0.42m,
                DoubleValue = 0.42d,
                EnumValue = EnumTestDataClass.Value2,
                FloatValue = 0.42f,
                StringValue = "Some text"
            };
            Sut.Add(element);
            var previousExistingElementCount = Sut.Query.Count();


            Sut.Delete(element);


            Sut.Query.Count().Should().Be(previousExistingElementCount-1);
            Sut.Query.FirstOrDefault(t => t.Id == element.Id).Should().BeNull();
        }
    }
}
