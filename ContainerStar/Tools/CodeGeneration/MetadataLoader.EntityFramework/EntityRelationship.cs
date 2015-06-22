using System.Resources;

namespace MetadataLoader.EntityFramework
{
    public sealed class EntityRelationship
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public EntityRelationship(EntityRelationSide from, EntityRelationSide to, bool willCascadeOnDelete, EntityInfo manyToManyTable = null)
        {
            From = from;
            To = to;
            WillCascadeOnDelete = willCascadeOnDelete;
            ManyToManyTable = manyToManyTable;
        }
        #endregion
        #region	Public properties
        public EntityRelationSide From { get; private set; }
        public EntityRelationSide To { get; private set; }
        public EntityInfo ManyToManyTable { get; set; }
        public bool WillCascadeOnDelete { get; private set; }
        #endregion
        #region	Public methods
        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} -> {1}", From, To);
        }
        #endregion
    }
}