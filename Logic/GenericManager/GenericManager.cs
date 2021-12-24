using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;
using Fateblade.Components.Data.GenericDataStoring.Contract;
using Fateblade.Components.Logic.GenericManager.Contract;
using System;
using System.Linq;

namespace Fateblade.Components.Logic.GenericManager
{
    class GenericManager<TEntity> : IGenericManager<TEntity> where TEntity : IIdentifiableGuidEntity
    {
        private readonly IGenericRepository<TEntity> _repository;



        public GenericManager(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }



        public IQueryable<TEntity> GetAll()
        {
            return _repository.Query;
        }

        public TEntity Get(Guid entityId)
        {
            return _repository.Query.First(t => t.Id == entityId);
        }

        public void Add(TEntity entity)
        {
            _repository.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }
    }
}
