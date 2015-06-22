using System;
using System.Diagnostics;

namespace MetadataLoader.Excel
{
  public sealed class TableDescriptor
  {
    #region Static
    public const int DynamicColumns = -1;

    public static TableDescriptor GetReadDynamic(string sheetName)
    {
      return GetRead(sheetName, DynamicColumns);
    }
    public static TableDescriptor GetReadDynamic(string sheetName, int keyColumnIndex, int beginRow)
    {
      return GetRead(sheetName, keyColumnIndex, beginRow, DynamicColumns);
    }
    public static TableDescriptor GetReadDynamic(string sheetName, TableKeyDescriptor key, int beginRow)
    {
      return GetRead(sheetName, key, beginRow, DynamicColumns);
    }

    public static TableDescriptor GetRead(string sheetName, int columnCount)
    {
      return GetRead(sheetName, 1, 2, columnCount);
    }
    public static TableDescriptor GetRead(string sheetName, int keyColumnIndex, int beginRow, int columnCount)
    {
      #region Check
      if (string.IsNullOrEmpty(sheetName))
      {
        throw new ArgumentNullException("sheetName");
      }
      if (keyColumnIndex <= 0)
      {
        throw new ArgumentException("Must be more than 0", "keyColumnIndex");
      }
      if (beginRow <= 0)
      {
        throw new ArgumentException("Must be more than 0", "beginRow");
      }
      #endregion
      return new TableDescriptor(sheetName, new TableKeyDescriptor(keyColumnIndex), beginRow, columnCount, null);
    }
    public static TableDescriptor GetRead(string sheetName, TableKeyDescriptor key, int beginRow, int columnCount)
    {
      #region Check
      if (string.IsNullOrEmpty(sheetName))
      {
        throw new ArgumentNullException("sheetName");
      }
      if (ReferenceEquals(key, null))
      {
        throw new ArgumentNullException("key");
      }
      if (beginRow <= 0)
      {
        throw new ArgumentException("Must be more than 0", "beginRow");
      }
      #endregion
      return new TableDescriptor(sheetName, key, beginRow, columnCount, null);
    }

    public static TableDescriptor GetWrite(string sheetName, params string[] columnNames)
    {
      return GetWrite(sheetName, 2, columnNames);
    }
    public static TableDescriptor GetWrite(string sheetName, int beginRow, params string[] columnNames)
    {
      #region Check
      if (string.IsNullOrEmpty(sheetName))
      {
        throw new ArgumentNullException("sheetName");
      }
      if (beginRow <= 0)
      {
        throw new ArgumentException("Must be more than 0", "beginRow");
      }
      if (ReferenceEquals(columnNames, null))
      {
        throw new ArgumentNullException("columnNames");
      }
      if (columnNames.Length == 0)
      {
        throw new ArgumentException("Columns' array is empty", "columnNames");
      }
      #endregion
      return new TableDescriptor(sheetName, null, beginRow, columnNames.Length, columnNames);
    }
    public static TableDescriptor GetReadWrite(string sheetName, int keyColumnIndex, int beginRow, params string[] columnNames)
    {
      #region Check
      if (string.IsNullOrEmpty(sheetName))
      {
        throw new ArgumentNullException("sheetName");
      }
      if (keyColumnIndex <= 0)
      {
        throw new ArgumentException("Must be more than 0", "keyColumnIndex");
      }
      if (beginRow <= 1)
      {
        throw new ArgumentException("Must be more than 0", "beginRow");
      }
      if (ReferenceEquals(columnNames, null))
      {
        throw new ArgumentNullException("columnNames");
      }
      if (columnNames.Length == 0)
      {
        throw new ArgumentException("Columns' array is empty", "columnNames");
      }
      #endregion
      return new TableDescriptor(sheetName, new TableKeyDescriptor(keyColumnIndex), beginRow, columnNames.Length, columnNames);
    }
    public static TableDescriptor GetReadWrite(string sheetName, params string[] columnNames)
    {
      return GetReadWrite(sheetName, 1, 2, columnNames);
    }
    #endregion
    #region Private fields
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string m_sheetName;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly TableKeyDescriptor m_key;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string[] m_columnNames;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int m_columnCount;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int m_beginRow;
    private readonly int m_headerRow;
    #endregion
    #region Constructor
    private TableDescriptor(string sheetName, TableKeyDescriptor key, int beginRow, int columnCount, string[] columnNames)
    {
      m_sheetName = sheetName;
      m_key = key;
      m_beginRow = beginRow;
      m_columnCount = columnCount;
      m_columnNames = columnNames;
      m_headerRow = m_beginRow - 1;
    }
    #endregion
    #region Public properties
    public bool CanRead
    {
      get { return !ReferenceEquals(m_key, null); }
    }
    public bool CanWrite
    {
      get { return !ReferenceEquals(m_columnNames, null); }
    }

    public int BeginRow
    {
      [DebuggerStepThrough]
      get { return m_beginRow; }
    }

    public int HeaderRow
    {
      get { return m_headerRow; }
    }
    /// <summary>
    ///   Sheet and table name
    /// </summary>
    public string SheetName
    {
      [DebuggerStepThrough]
      get { return m_sheetName; }
    }
    /// <summary>
    ///   Columns' names
    /// </summary>
    public string[] ColumnNames
    {
      [DebuggerStepThrough]
      get { return m_columnNames; }
    }
    /// <summary>
    ///   Count of reading columns. Begin from top left sheet's corner
    /// </summary>
    public int ColumnCount
    {
      [DebuggerStepThrough]
      get { return m_columnCount; }
    }

    /// <summary>
    ///   Count of reading columns. Begin from top left sheet's corner
    /// </summary>
    public bool DynamicColumnCount
    {
      [DebuggerStepThrough]
      get { return m_columnCount <= 0; }
    }
    /// <summary>
    ///   Index of key column. If that column's cell is empty - stop reading
    /// </summary>
    public TableKeyDescriptor Key
    {
      [DebuggerStepThrough]
      get { return m_key; }
    }
    #endregion
  }
}