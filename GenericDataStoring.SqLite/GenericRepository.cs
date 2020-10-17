using DavidTielke.PersonManagementApp.CrossCutting.CoCo.Core.Contract.EventBrokerage;
using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;
using Fateblade.Components.Data.GenericDataStoring.Contract;
using Fateblade.Components.Data.GenericDataStoring.Contract.Messages;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace Fateblade.Components.Data.GenericDataStoring.SqLite
{
    internal class GenericRepository<TEntity> : DbContext, IGenericRepository<TEntity> 
        where TEntity : class, IIdentifiableGuidEntity
    {
        //members
        private readonly IEventBroker _eventBroker;
        private readonly GenericDataSqLiteStoringConfiguration _configuration;
        private string _completeDbPath;



        //properties
        public IQueryable<TEntity> Query => throw new NotImplementedException();
        public DbSet<TEntity> Entities { get; set; }



        //ctors
        public GenericRepository(IEventBroker eventBroker, GenericDataSqLiteStoringConfiguration configuration)
        {
            _eventBroker = eventBroker;
            _configuration = configuration;
            initialize();
        }

        

        //public methods
        public void Add(TEntity entity)
        {
            base.Add(entity);
            base.SaveChanges();

            _eventBroker.Raise(new EntityChangedMessage<TEntity>
            {
                ChangeType = ChangeType.Created,
                Entity = entity
            });
        }

        public void Delete(TEntity entity)
        {
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
            TEntity foundEntity = Entities.FirstOrDefault(existingEntity => entity.Id == existingEntity.Id);
            if (foundEntity != null)
            {
                //Todo: how do i update properties of a generic entity?
                //Todo: new component? -> every store needs to implement this converting component for its models then

                //for now .... just do a save?
                base.SaveChanges();

                _eventBroker.Raise(new EntityChangedMessage<TEntity>
                {
                    ChangeType = ChangeType.Updated,
                    Entity = entity
                });
            }
            else
            {
                this.Add(entity);
            }
        }



        //protected methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_completeDbPath}");
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
