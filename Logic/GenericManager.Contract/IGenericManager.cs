using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;
using System;
using System.Linq;

namespace Fateblade.Components.Logic.GenericManager.Contract
{
    public interface IGenericManager<TEntity> where TEntity : IIdentifiableGuidEntity
    {
        IQueryable<TEntity> GetAll();
        TEntity Get(Guid entityId);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
