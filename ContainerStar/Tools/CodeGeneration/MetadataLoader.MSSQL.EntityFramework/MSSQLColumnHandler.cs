using MetadataLoader.Contracts.Database;
using MetadataLoader.MSSQL.Contracts.Database;
using MetadataLoader.EntityFramework;

namespace MetadataLoader.MSSQL.EntityFramework
{
    public class MSSQLColumnHandler<TTableContent, TColumnContent> : IEntityHandler<MSSQLColumn<TTableContent, TColumnContent>, SimplePropertyEntityInfo>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        public void Run(MSSQLColumn<TTableContent, TColumnContent> column, SimplePropertyEntityInfo obj)
        {
            obj.IsUnicode = column.TypeDescription.IsUnicode;
            obj.IsRowGuid = column.IsRowGuid;
            #region Length
            if (column.TypeDescription.HasLength)
            {
                obj.Length = column.Length;
                obj.IsFixedLength = column.TypeDescription.IsFixedLength;
            }
            #endregion
            #region DatabaseGeneratedOption
            switch (column.ColumnType)
            {
                case ColumnTypes.Identity:
                    obj.DatabaseGeneratedOption = DatabaseGeneratedOption.Identity;
                    break;
                case ColumnTypes.IsComputed:
                    obj.DatabaseGeneratedOption = DatabaseGeneratedOption.Computed;
                    break;
            }
            #endregion
        }
    }
}