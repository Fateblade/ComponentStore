namespace Fateblade.Components.Data.GenericDataStoring.MariaDb.Tests.GenericRepository
{
    [TestFixture]
    public partial class StandardGuidGenericRepositoryTests : GenericRepositoryTestBase<GuidTestDataClass>
    {
        public StandardGuidGenericRepositoryTests()
        {
            PropertyUpdater = new GuidTestDataClassPropertyUpdater();
        }

        [Test]
        public void Initialization_CompletesSuccessfully()
        {
            Assert.Pass();
        }
    }
}