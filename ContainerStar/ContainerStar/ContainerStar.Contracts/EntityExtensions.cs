using System.Diagnostics.Contracts;

namespace ContainerStar.Contracts
{
    /// <summary>
    /// Entity extensions
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Determines whether the specified entity is a new one. 
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static bool IsNew( this IHasId<int> entity )
        {
            Contract.Requires(entity != null);
            // This check is not entirely correct, but now there is no better way
            return entity.Id.IsNewId();
        }

        /// <summary>
        /// Determines whether the specified entity identifier is a new one. 
        /// </summary>
        /// <param name="entityId">The entity indentifier.</param>
        /// <returns></returns>
        public static bool IsNewId(this int entityId)
        {
            // This check is not entirely correct, but now there is no better way
            return Equals(entityId, default(int));
        }

        /// <summary>
        /// Determines whether the specified entity identifier is a new one. 
        /// </summary>
        /// <param name="entityId">The entity indentifier.</param>
        /// <returns></returns>
        public static bool IsNewId(this int? entityId)
        {
            return !entityId.HasValue || entityId.Value.IsNewId();
        }

        /// <summary>
        /// Identifiers the or null.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static int? IdOrNull( this IHasId<int> entity )
        {
            return entity != null ? (int?)entity.Id : null;
        }
    }
}
