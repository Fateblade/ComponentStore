namespace Fateblade.Components.Data.GenericDataStoring.SqLite.Tests.ForeignKey
{
    [TestFixture]
    public partial class ForeignKeyGuidGenericRepositoryTests : GenericRepositoryTestBase<GuidForeignTestDataClass>
    {
        public ForeignKeyGuidGenericRepositoryTests()
        {
            PropertyUpdater = new GuidForeignTestDataClassPropertyUpdater();
        }

        [Test]
        public void Initialization_CompletesSuccessfully()
        {
            Assert.Pass();
        }
    }
}