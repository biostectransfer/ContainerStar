using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using ContainerStar.Contracts;
using ContainerStar.Contracts.SaveActors.Base;


namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Database context for ContainerStar
    /// </summary>
    public abstract class EntitiesBase : DbContext
    {
        private readonly ISaveActorManagerBase saveActorManager;
        /// <summary>
        /// Constructs a new context instance using conventions to create the name of the database to
        ///             which a connection will be made.  The by-convention name is the full name (namespace + class name)
        ///             of the derived context class.
        ///             See the class remarks for how this is used to create a connection.
        /// </summary>
        protected EntitiesBase()
        {
        }

        /// <summary>
        /// Constructs a new context instance using the given string as the name or connection string for the
        ///             database to which a connection will be made.
        ///             See the class remarks for how this is used to create a connection.
        /// </summary>
        /// <param name="saveActorManager">Manage all save actors inside</param>
        /// <param name="nameOrConnectionString">Either the database name or a connection string. </param>
        protected EntitiesBase(ISaveActorManagerBase saveActorManager, string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.saveActorManager = saveActorManager;
        }
        /// <summary>
        ///     Gets set of entities
        /// </summary>
        /// <typeparam name="TEntity">type of entities</typeparam>
        /// <returns></returns>
        public IQueryable<TEntity> GetSet<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }

        /// <summary>
        ///     Add entity in context
        /// </summary>
        /// <typeparam name="TEntity">Type of adding entity</typeparam>
        /// <param name="entity">Adding entity</param>
        public void AddObject<TEntity>(TEntity entity)
            where TEntity : class
        {
            Set<TEntity>().Add(entity);
        }

        /// <summary>
        ///     Create entity without adding it to context. Use <see cref="IEntities.AddObject{TEntity}" /> to add object to
        ///     context
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public TEntity CreateObject<TEntity>() where TEntity : class
        {
            return Set<TEntity>().Create();
        }

        /// <summary>
        ///     Update entity in context
        /// </summary>
        /// <typeparam name="TEntity">Type of updating entity</typeparam>
        /// <param name="entity">Updating entity</param>
        public void UpdateObject<TEntity>(TEntity entity)
            where TEntity : class
        {
            //var entry = Entry(entity);
            //entry.State = EntityState.Modified;
        }

        /// <summary>
        ///     Delete entity in context
        /// </summary>
        /// <typeparam name="TEntity">Type of deleting entity</typeparam>
        /// <param name="entity">Deleting entity</param>
        public void DeleteObject<TEntity>(TEntity entity)
            where TEntity : class
        {
            Set<TEntity>().Remove(entity);
        }

        /// <summary>
        ///     Saves changes only
        /// </summary>
        protected void SaveChangesInternal()
        {
            try
            {
                foreach (var dbEntityEntry in ChangeTracker.Entries())
                {
                    saveActorManager.DoBeforeSaveAction(dbEntityEntry.Entity, dbEntityEntry.State);
                }        
        
                base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
#if DEBUG
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName,
                            ve.ErrorMessage);
                    }
                }
#endif //DEBUG
                throw;
            }
        }

        /// <summary>
        ///     Add entity to context
        /// </summary>
        /// <typeparam name="TEntity">Type of adding entity</typeparam>
        /// <param name="entities">Adding entities</param>
        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }

        /// <summary>
        ///     Save all changes from underlying context to database
        /// </summary>
        public new virtual void SaveChanges()
        {
            SaveChangesInternal();
        }

        /// <summary>
        ///     Object context
        /// </summary>
        public ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter)this).ObjectContext; }
        }

        /// <summary> Throws InvalidOperationException if there is any change in DbContext after the last changes saving. </summary>
        public void ThrowIfHasChange()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.State != EntityState.Unchanged)
                {
                    throw new InvalidOperationException(
                        string.Format("Unexpected FeEntities' changes are detected for entity of type: {0}",
                            item.Entity == null ? null : item.Entity.GetType().FullName));
                }
            }
        }

        protected virtual void RegisterCustomMappings(DbModelBuilder modelBuilder)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            RegisterCustomMappings(modelBuilder);
        }
    }
}