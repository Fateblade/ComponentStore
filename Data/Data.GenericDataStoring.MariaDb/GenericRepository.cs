using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;
using Fateblade.Components.Data.GenericDataStoring.Contract;
using Fateblade.Components.Data.GenericDataStoring.Contract.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Fateblade.Components.Data.GenericDataStoring.MariaDb
{
    
    internal class GenericRepository<TEntity> : DbContext, IGenericRepository<TEntity> 
        where TEntity : class, IIdentifiableGuidEntity
    {
        //members
        private readonly IEventBroker _eventBroker;
        private readonly IPropertyUpdater<TEntity> _entityPropertyUpdater;
        private readonly GenericDataMariaDbStoringConfiguration _configuration;



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
        public GenericRepository(IEventBroker eventBroker, IPropertyUpdater<TEntity> entityPropertyUpdater, GenericDataMariaDbStoringConfiguration configuration)
        {
            if (!typeof(TEntity).GetCustomAttributes(true).Any(t => t is TableAttribute))
            {
                throw new ArgumentException(
                    $"Generic repository implementation for MariaDb needs the classes to have a defined attribute System.ComponentModel.DataAnnotations.Schema.TableAttribute (i.e. [Table(nameof(ClassName)]");
            }

            _eventBroker = eventBroker;
            _entityPropertyUpdater = entityPropertyUpdater;
            _configuration = configuration;
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

        internal void DropDatabase()
        {
            Database.EnsureDeleted();
        }



        //protected methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = $"server={_configuration.DbServerName};user={_configuration.UserName};password={_configuration.Password};database={_configuration.DbName}";
            var serverVersion = new MariaDbServerVersion(ServerVersion.AutoDetect(connectionString));
            optionsBuilder.UseMySql(connectionString, serverVersion);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
