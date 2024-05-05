using System.Collections.Generic;
using System.Linq;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.Aspects;
using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;
using Fateblade.Components.Data.GenericDataStoring.Contract.Exceptions;

namespace Fateblade.Components.Data.GenericDataStoring.Contract
{
    [MapException(typeof(GenericDataStoringException))]
    public interface IGenericRepository<TEntity> where TEntity: IIdentifiableGuidEntity
    {
        [ExceptionMessage("Entity could not be added")]
        void Add(TEntity entity);
        [ExceptionMessage("Entities could not be added")]
        void AddRange(IEnumerable<TEntity> entity);
        [ExceptionMessage("Entity could not be updated")]
        void Update(TEntity entity);
        [ExceptionMessage("Entity could not be deleted")]
        void Delete(TEntity entity);
        IQueryable<TEntity> Query { get; }
    } 
}
