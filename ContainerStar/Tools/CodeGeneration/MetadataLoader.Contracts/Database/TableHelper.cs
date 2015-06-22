namespace MetadataLoader.Contracts.Database
{
    public static class TableHelper<TTable, TTableContent, TColumn, TColumnContent>
        where TTable : Table<TTable, TTableContent, TColumn, TColumnContent>
        where TColumn : Column<TTable, TTableContent, TColumn, TColumnContent>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        public static void AddColumn(TTable table, TColumn column)
        {
            table.AddColumn(column);
        }

        public static void AddRelationship(TTable fromTable, TColumn[] fromColumns, TTable toTable, TColumn[] toColumns, bool willCascadeOnDelete = false)
        {
            var relationship = new Relationship<TTable, TTableContent, TColumn, TColumnContent>(fromTable, fromColumns, toTable, toColumns, willCascadeOnDelete);

            fromTable.AddFromRelationship(relationship);
            toTable.AddToRelationship(relationship);
        }
    }
}