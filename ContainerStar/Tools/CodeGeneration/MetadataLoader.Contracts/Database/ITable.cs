using System.Collections.Generic;

namespace MetadataLoader.Contracts.Database
{
    public interface ITable : IBaseObject
    {
        string Schema { get; }
    }

    public interface ITable<out T, out TColumnContent> : ITable, IBaseObject<T>
    {
        IReadOnlyCollection<IColumn<TColumnContent>> Columns { get; }
    }
}