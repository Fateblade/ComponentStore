﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;
using Fateblade.Components.Data.GenericDataStoring.Contract;
using Fateblade.Components.Data.GenericDataStoring.Contract.Messages;

namespace Fateblade.Components.Data.GenericDataStoring.Text.Json
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity:IIdentifiableGuidEntity
    {
        //members
        private readonly IEventBroker _eventBroker;
        private readonly string _fileName = typeof(TEntity).Name + ".json";
        private readonly JsonSerializerOptions _serializerOptions;

        private List<TEntity> _entities;
        private readonly string _rootPath;
        private readonly string _completePath;
        private bool _justSentMessage;



        //properties
        public IQueryable<TEntity> Query => _entities.AsQueryable();



        //ctors
        public GenericRepository(IEventBroker eventBroker, GenericDataStoringConfiguration configuration)
        {
            _eventBroker = eventBroker;

            _rootPath = String.IsNullOrWhiteSpace(configuration.RootDirectoryPath)
                ? Path.Combine(Directory.GetCurrentDirectory(), "Data")
                : configuration.RootDirectoryPath;

            _completePath = Path.Combine(_rootPath, _fileName);

            _serializerOptions = new JsonSerializerOptions();

            // ReSharper disable once VirtualMemberCallInConstructor
            ConfigureSerializerOptions(_serializerOptions);

            initializeEntitiesFromFile();
            _eventBroker.Subscribe<EntityChangedMessage<TEntity>>(handleEntityChangedMessage);
        }



        //public methods
        public void Add(TEntity entity)
        {
            generateIdAndAddEntity(entity);
            save();

            _justSentMessage = true;
            _eventBroker.Raise(new EntityChangedMessage<TEntity>
            {
                ChangeType = ChangeType.Created,
                Entity = entity
            });
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            var entityArray = entities.ToArray();
            foreach (var entity in entityArray)
            {
                generateIdAndAddEntity(entity);
            }
            save();

            _justSentMessage = true;
            _eventBroker.Raise(new EntitiesChangedMessage<TEntity>
            {
                ChangeType = ChangeType.Created,
                Entity = entityArray
            });
        }

        public void Update(TEntity entity)
        {
            var indexOfEntity = getIndexOfEntity(entity);
            _entities[indexOfEntity] = entity;
            save();

            _justSentMessage = true;
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

            _justSentMessage = true;
            _eventBroker.Raise(new EntityChangedMessage<TEntity>
            {
                ChangeType = ChangeType.Removed,
                Entity = entity
            });
        }



        //protected methods
        /// <summary>
        /// Used to configure the json serialization options.
        /// Be aware, this method is called during construction of the object!
        /// </summary>
        /// <param name="configurableOptions"></param>
        protected virtual void ConfigureSerializerOptions(JsonSerializerOptions configurableOptions){ }


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
                    _entities = JsonSerializer.Deserialize<List<TEntity>>(sr.ReadToEnd(), _serializerOptions);
                }
            }
        }

        private void generateIdAndAddEntity(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            _entities.Add(entity);
        }

        private void save()
        {
            using (var sw = new StreamWriter(File.Open(_completePath, FileMode.Create)))
            {
                sw.Write(JsonSerializer.Serialize(_entities, _serializerOptions));
            }
        }

        private int getIndexOfEntity(TEntity entityToFind)
        {
            return _entities.FindIndex(entity => entity.Id.Equals(entityToFind.Id));
        }

        private void handleEntityChangedMessage(EntityChangedMessage<TEntity> entityChangedMessage)
        {
            if (_justSentMessage)
            {
                _justSentMessage = false;
                return;
            }

            switch (entityChangedMessage.ChangeType)
            {
                case ChangeType.Created:
                    handleEntityAdded(entityChangedMessage.Entity);
                    break;
                case ChangeType.Updated:
                    handleEntityUpdated(entityChangedMessage.Entity);
                    break;
                case ChangeType.Removed:
                    handleEntityRemoved(entityChangedMessage.Entity); 
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unknown change type '{entityChangedMessage.ChangeType}'");
            }
        }

        private void handleEntityRemoved(TEntity entity)
        {
            var indexOfEntity = getIndexOfEntity(entity);
            if (indexOfEntity == -1) return;

            _entities.RemoveAt(indexOfEntity);
            //save not necessary because it was already saved by the sender of this message
        }

        private void handleEntityUpdated(TEntity entity)
        {
            var indexOfEntity = getIndexOfEntity(entity);
            if (indexOfEntity == -1)
            {
                handleEntityAdded(entity);
                return;
            }

            _entities[indexOfEntity] = entity;
            //save not necessary because it was already saved by the sender of this message
        }

        private void handleEntityAdded(TEntity entity)
        {
            if (entity.Id == Guid.Empty) throw new ArgumentException($"Added entity has to have a valid id", nameof(entity.Id));
            
            var indexOfEntity = getIndexOfEntity(entity);
            if (indexOfEntity != -1)
            {
                handleEntityUpdated(entity);
                return;
            }

            _entities.Add(entity);

            //save not necessary because it was already saved by the sender of this message
        }
    }
}