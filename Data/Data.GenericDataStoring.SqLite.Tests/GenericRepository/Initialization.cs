using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;

namespace Fateblade.Components.Data.GenericDataStoring.SqLite.Tests.GenericRepository
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