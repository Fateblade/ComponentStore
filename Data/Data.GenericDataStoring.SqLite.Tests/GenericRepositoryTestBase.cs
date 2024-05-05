using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using FakeItEasy;
using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;
using Fateblade.Components.Data.GenericDataStoring.Contract;

namespace Fateblade.Components.Data.GenericDataStoring.SqLite.Tests;

public abstract class GenericRepositoryTestBase<TIdentifiable> where TIdentifiable : class, IIdentifiableGuidEntity
{
    private IEventBroker _eventBrokerMock;
    private GenericDataSqLiteStoringConfiguration _testConfiguration;

    protected IPropertyUpdater<TIdentifiable> PropertyUpdater;

#pragma warning disable NUnit1032
    protected IGenericRepository<TIdentifiable> Sut { get; private set; }
#pragma warning restore NUnit1032


    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _eventBrokerMock = A.Fake<IEventBroker>();
        _testConfiguration = new GenericDataSqLiteStoringConfiguration
        {
            DbDirectoryPath = Directory.GetCurrentDirectory(),
            DbName = "testDb.db"
        };
    }


    [SetUp]
    public void SetUp()
    {
        Sut = new GenericRepository<TIdentifiable>(_eventBrokerMock, PropertyUpdater, _testConfiguration);
    }

    [TearDown]
    public void TearDown()
    {
        Sut = null;

        var dbFile = Path.Combine(Directory.GetCurrentDirectory(), "testDb.db");

        if (File.Exists(dbFile))
        {
            File.Delete(dbFile);
        }
        
    }
}