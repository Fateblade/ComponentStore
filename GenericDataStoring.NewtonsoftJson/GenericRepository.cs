using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;
using Fateblade.Components.Data.GenericDataStoring.Contract;
using Fateblade.Components.Data.GenericDataStoring.Contract.Messages;
using Newtonsoft.Json;

namespace Fateblade.Components.Data.GenericDataStoring.NewtonsoftJson
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity:IIdentifiableGuidEntity
    {
        //members
        private readonly IEventBroker _eventBroker;
        private readonly GenericDataStoringConfiguration _configuration;
        private readonly string _fileName = typeof(TEntity).Name + ".json";
        private List<TEntity> _entities;



        //properties
        public IQueryable<TEntity> Query => _entities.AsQueryable();



        //ctors
        public GenericRepository(IEventBroker eventBroker, GenericDataStoringConfiguration configuration)
        {
            _eventBroker = eventBroker;
            _configuration = configuration;
            initializeEntitiesFromFile();
        }

        

        //public methods
        public void Add(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            _entities.Add(entity);
            save();

            _eventBroker.Raise(new EntityChangedMessage<TEntity>
            {
                ChangeType = ChangeType.Created,
                Entity = entity
            });
        }

        public void Update(TEntity entity)
        {
            var indexOfEntity = getIndexOfEntity(entity);
            _entities[indexOfEntity] = entity;
            save();

            _eventBroker.Raise(new EntityChangedMessage<TEntity>
            {
                ChangeType = ChangeType.Updated,
                Entity = entity
            });
        }

        public void Delete(TEntity entity)
        {
            var indexOfEntity = getIndexOfEntity(entity);
            _entities.RemoveAt(indexOfEntity);
            save();

            _eventBroker.Raise(new EntityChangedMessage<TEntity>
            {
                ChangeType = ChangeType.Removed,
                Entity = entity
            });
        }



        //private methods
        private void initializeEntitiesFromFile()
        {
            string completePath = Path.Combine(_configuration.RootDirectoryPath, _fileName);
            if (!Directory.Exists(_configuration.RootDirectoryPath))
            {
                Directory.CreateDirectory(_configuration.RootDirectoryPath);
            }

            if (!File.Exists(completePath))
            {
                _entities = new List<TEntity>();
            }
            else
            {
                using (var sr = new StreamReader(File.Open(completePath, FileMode.OpenOrCreate)))
                {
                    _entities = JsonConvert.DeserializeObject<List<TEntity>>(sr.ReadToEnd());
                }
            }
        }

        private void save()
        {
            string completePath = Path.Combine(_configuration.RootDirectoryPath, _fileName);
            using (var sw = new StreamWriter(File.Open(completePath, FileMode.Create)))
            {
                sw.Write(JsonConvert.SerializeObject(_entities));
            }
        }

        private int getIndexOfEntity(TEntity entityToFind)
        {
            return _entities.FindIndex(entity => entity.Id.Equals(entityToFind.Id));
        }
    }
}
