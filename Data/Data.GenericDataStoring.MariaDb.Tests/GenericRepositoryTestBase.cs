using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using FakeItEasy;
using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;
using Fateblade.Components.Data.GenericDataStoring.Contract;

namespace Fateblade.Components.Data.GenericDataStoring.MariaDb.Tests;

public abstract class GenericRepositoryTestBase<TIdentifiable> where TIdentifiable : class, IIdentifiableGuidEntity
{
    protected IEventBroker EventBrokerMock;
    protected GenericDataMariaDbStoringConfiguration TestConfiguration;

    protected IPropertyUpdater<TIdentifiable> PropertyUpdater;

#pragma warning disable NUnit1032
    protected IGenericRepository<TIdentifiable> Sut { get; private set; }
#pragma warning restore NUnit1032


    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        EventBrokerMock = A.Fake<IEventBroker>();
        TestConfiguration = new GenericDataMariaDbStoringConfiguration()
        {
            DbServerName = "",
            UserName = "",
            Password = "",
            DbName = "efCoreTestDb"
        };
    }


    [SetUp]
    public void SetUp()
    {
        Sut = new GenericRepository<TIdentifiable>(EventBrokerMock, PropertyUpdater, TestConfiguration);
    }

    [TearDown]
    public void TearDown()
    {
        (Sut as GenericRepository<TIdentifiable>)?.DropDatabase();
        Sut = null;
    }
}