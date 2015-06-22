using MetadataLoader.Contracts.Database;
using MetadataLoader.MSSQL.Contracts.Database;
using MetadataLoader.EntityFramework;

namespace MetadataLoader.MSSQL.EntityFramework
{
    public class MSSQLColumnDefaultValuetHandler<TTableContent, TColumnContent> : IEntityHandler<MSSQLColumn<TTableContent, TColumnContent>, SimplePropertyEntityInfo>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        public void Run(MSSQLColumn<TTableContent, TColumnContent> column, SimplePropertyEntityInfo obj)
        {
            if (string.IsNullOrEmpty(column.Default))
            {
                obj.DefaultValue = DbDefaultToCSharpConverter.ConvertDefault(column.TypeDescription.BaseType, column.Default);
                //TODO: Log failed default convertation
            }
        }
    }
}