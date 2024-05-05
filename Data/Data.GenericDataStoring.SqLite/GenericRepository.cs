using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;
using Fateblade.Components.Data.GenericDataStoring.Contract;
using Fateblade.Components.Data.GenericDataStoring.Contract.Messages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace Fateblade.Components.Data.GenericDataStoring.SqLite
{
    
    internal class GenericRepository<TEntity> : DbContext, IGenericRepository<TEntity> 
        where TEntity : class, IIdentifiableGuidEntity
    {
        //members
        private readonly IEventBroker _eventBroker;
        private readonly IPropertyUpdater<TEntity> _entityPropertyUpdater;
        private readonly GenericDataSqLiteStoringConfiguration _configuration;
        private string _completeDbPath;



        //properties
        public IQueryable<TEntity> Query
        {
            get
            {
                Database.EnsureCreated();
                return Entities.AsQueryable();
            }
        }


        public DbSet<TEntity> Entities { get; set; }



        //ctors
        public GenericRepository(IEventBroker eventBroker, IPropertyUpdater<TEntity> entityPropertyUpdater, GenericDataSqLiteStoringConfiguration configuration)
        {
            if (!typeof(TEntity).GetCustomAttributes(true).Any(t => t is TableAttribute))
            {
                throw new ArgumentException(
                    $"Generic repository implementation for SqLite needs the classes to have a defined attribute System.ComponentModel.DataAnnotations.Schema.TableAttribute (i.e. [Table(nameof(ClassName)]");
            }

            _eventBroker = eventBroker;
            _entityPropertyUpdater = entityPropertyUpdater;
            _configuration = configuration;
            initialize();
        }

        

        //public methods
        public void Add(TEntity entity)
        {
            Database.EnsureCreated();

            base.Add(entity);
            base.SaveChanges();

            _eventBroker.Raise(new EntityChangedMessage<TEntity>
            {
                ChangeType = ChangeType.Created,
                Entity = entity
            });
        }

        public void AddRange(IEnumerable<TEntity> entity)
        {
            Database.EnsureCreated();

            base.AddRange(entity);
            base.SaveChanges();

            _eventBroker.Raise(new EntitiesChangedMessage<TEntity>
            {
                ChangeType = ChangeType.Created,
                Entity = entity
            });
        }

        public void Delete(TEntity entity)
        {
            Database.EnsureCreated();

            base.Remove(entity);
            base.SaveChanges();

            _eventBroker.Raise(new EntityChangedMessage<TEntity>
            {
                ChangeType = ChangeType.Removed,
                Entity = entity
            });
        }

        public void Update(TEntity entity)
        {
            Database.EnsureCreated();

            var foundEntity = Entities.FirstOrDefault(existingEntity => entity.Id == existingEntity.Id);
            if (foundEntity != null)
            {
                _entityPropertyUpdater.UpdateProperties(entity, foundEntity);

                base.SaveChanges();

                _eventBroker.Raise(new EntityChangedMessage<TEntity>
                {
                    ChangeType = ChangeType.Updated,
                    Entity = entity
                });
            }
            else
            {
                Add(entity);
            }
        }



        //protected methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_completeDbPath};");
            base.OnConfiguring(optionsBuilder);
        }

        //private methods
        private void initialize()
        {
            _completeDbPath = String.IsNullOrWhiteSpace(_configuration.DbDirectoryPath)
                ? Path.Combine(Directory.GetCurrentDirectory(), _configuration.DbName)
                : Path.Combine(_configuration.DbDirectoryPath, _configuration.DbName);

            if (!Directory.Exists(_configuration.DbDirectoryPath))
            {
                Directory.CreateDirectory(_configuration.DbDirectoryPath);
            }
        }
    }
}
