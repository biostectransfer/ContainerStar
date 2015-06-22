using System;

namespace MetadataLoader.Content
{
    [Flags]
    public enum ContentLoadType
    {
        None = 0,
        Table = 1,
        Column = 2,
        TableOrColumn = 3
    }
}