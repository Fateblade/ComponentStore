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
        private readonly string _rootPath;
        private readonly string _completePath;


        //properties
        public IQueryable<TEntity> Query => _entities.AsQueryable();



        //ctors
        public GenericRepository(IEventBroker eventBroker, GenericDataStoringConfiguration configuration)
        {
            _eventBroker = eventBroker;
            _configuration = configuration;

            _rootPath = String.IsNullOrWhiteSpace(_configuration.RootDirectoryPath)
                ? Directory.GetCurrentDirectory()
                : _configuration.RootDirectoryPath;

            _completePath = Path.Combine(_rootPath, _fileName);


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
            if (!Directory.Exists(_rootPath))
            {
                Directory.CreateDirectory(_rootPath);
            }

            if (!File.Exists(_completePath))
            {
                _entities = new List<TEntity>();
            }
            else
            {
                using (var sr = new StreamReader(File.Open(_completePath, FileMode.OpenOrCreate)))
                {
                    _entities = JsonConvert.DeserializeObject<List<TEntity>>(sr.ReadToEnd());
                }
            }
        }

        private void save()
        {
            using (var sw = new StreamWriter(File.Open(_completePath, FileMode.Create)))
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
