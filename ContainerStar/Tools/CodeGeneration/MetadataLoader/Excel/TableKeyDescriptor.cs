using System.Diagnostics;

namespace MetadataLoader.Excel
{
  public sealed class TableKeyDescriptor
  {
    #region Static
    ///<summary>
    /// Singleton instance 
    ///</summary>
    public static readonly TableKeyDescriptor Default = new TableKeyDescriptor(1);
    #endregion
    #region Private fields
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int[] _mItems;
    #endregion
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="items"></param>
    public TableKeyDescriptor(params int[] items)
    {
      _mItems = items;
    }
    #endregion
    #region Public properties
    public int[] Items
    {
      [DebuggerStepThrough]
      get { return _mItems; }
    }
    #endregion
  }
}