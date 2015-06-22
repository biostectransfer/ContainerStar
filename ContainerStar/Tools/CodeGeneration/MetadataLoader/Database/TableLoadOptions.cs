using System;

namespace MetadataLoader.Database
{
    [Flags]
    public enum TableLoadOptions
    {
        None = 0,
        /// <summary>
        ///     Skip relationships from load
        /// </summary>
        SkipRelationships = 0x1,
        /// <summary>
        ///     Skip broken relationships(with missed to table) from load
        /// </summary>
        SkipBrokenRelationship = 0x2
    }
}