using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;

namespace MetadataLoader.Contracts.Database
{
    public class Table<TTable, TTableContent, TColumn, TColumnContent> : Base<TTableContent>
        , ITable<TTableContent, TColumnContent>
        where TTable : Table<TTable, TTableContent, TColumn, TColumnContent>
        where TColumn : Column<TTable, TTableContent, TColumn, TColumnContent>
        where TTableContent : IContent, new()
        where TColumnContent : IContent, new()
    {
        #region Private fields
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<TColumn> _columns;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<Relationship<TTable, TTableContent, TColumn, TColumnContent>> _fromRelations;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<Relationship<TTable, TTableContent, TColumn, TColumnContent>> _toRelations;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public Table()
        {
            _columns = new List<TColumn>();
            _fromRelations = new List<Relationship<TTable, TTableContent, TColumn, TColumnContent>>();
            _toRelations = new List<Relationship<TTable, TTableContent, TColumn, TColumnContent>>();
        }
        #endregion
        #region Public properties
        public string Schema { get; set; }

        public string FullName
        {
            get { return string.IsNullOrWhiteSpace(Schema) ? Name : Schema + "." + Name; }
        }
        public bool HasKey
        {
            get { return Columns.Any(column => column.IsKeyColumn); }
        }

        public bool HasCompositeKey
        {
            get { return Columns.Count(column => column.IsKeyColumn) > 1; }
        }

        public TColumn this[string name]
        {
            get { return _columns.FirstOrDefault(column => column.Name == name); }
        }

        public IReadOnlyCollection<TColumn> Columns
        {
            get { return _columns; }
        }

        IReadOnlyCollection<IColumn<TColumnContent>> ITable<TTableContent, TColumnContent>.Columns
        {
            get { return Columns; }
        }

        public IReadOnlyCollection<Relationship<TTable, TTableContent, TColumn, TColumnContent>> FromRelations
        {
            get { return _fromRelations; }
        }

        public IReadOnlyCollection<Relationship<TTable, TTableContent, TColumn, TColumnContent>> ToRelations
        {
            get { return _toRelations; }
        }
        #endregion
        #region Internal methods
        internal void AddColumn(TColumn column)
        {
            if (_columns.Any(col => col.Name == column.Name))
            {
                throw new ArgumentException(string.Format("Column with name {0} already exists in table {1}", column.Name, Name));
            }
            _columns.Add(column);
        }

        internal void AddFromRelationship(Relationship<TTable, TTableContent, TColumn, TColumnContent> relationship)
        {
            Contract.Requires(relationship != null);
            Contract.Requires(ReferenceEquals(relationship.FromTable, this));

            _fromRelations.Add(relationship);
        }

        internal void AddToRelationship(Relationship<TTable, TTableContent, TColumn, TColumnContent> relationship)
        {
            Contract.Requires(relationship != null);
            Contract.Requires(ReferenceEquals(relationship.ToTable, this));

            _toRelations.Add(relationship);
        }
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
            return FullName;
        }
        #endregion
    }
}