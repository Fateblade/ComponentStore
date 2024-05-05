namespace Fateblade.Components.Data.GenericDataStoring.MariaDb.Tests.CrossTables
{
    [TestFixture]
    public partial class ForeignKeyGuidGenericRepositoryTests : GenericRepositoryTestBase<GuidCrossTestDataClass>
    {
        private GenericRepository<GuidTestDataClass> _guidTestDataClassRepository;
        private GenericRepository<GuidTestDataClass> GuidTestDataClassRepository
        {
            get
            {
                if (_guidTestDataClassRepository == null)
                {
                    _guidTestDataClassRepository = new GenericRepository<GuidTestDataClass>(EventBrokerMock, null, TestConfiguration);
                }

                return _guidTestDataClassRepository;
            }
        }

        public ForeignKeyGuidGenericRepositoryTests()
        {
            PropertyUpdater = new GuidCrossTestDataClassPropertyUpdater();
        }

        [Test]
        public void Initialization_CompletesSuccessfully()
        {
            Assert.Pass();
        }
    }
}