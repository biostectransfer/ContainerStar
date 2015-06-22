using System.Collections.Generic;

namespace ContainerStar.Contracts
{
    public interface ISaveManager
    {
        /// <summary>
        /// Add entity to context 
        /// </summary>
        /// <typeparam name="TEntity">Type of adding entity</typeparam>
        /// <param name="entity">Adding entity</param>
        void AddEntity<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Add entity to context 
        /// </summary>
        /// <typeparam name="TEntity">Type of adding entity</typeparam>
        /// <param name="entity">Adding entity</param>
        void AddEntities<TEntity>(IEnumerable<TEntity> entity) where TEntity : class;

        /// <summary>
        /// Update entity in context 
        /// </summary>
        /// <typeparam name="TEntity">Type of updating entity</typeparam>
        /// <param name="entity">Updating entity</param>
        void UpdateEntity<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Delete entity in context 
        /// </summary>
        /// <typeparam name="TEntity">Type of deleting entity</typeparam>
        /// <param name="entity">Deleting entity</param>
        void DeleteEntity<TEntity>(TEntity entity) where TEntity : class;
    }
}