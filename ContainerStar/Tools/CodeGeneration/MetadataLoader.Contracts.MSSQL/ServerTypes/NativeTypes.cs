namespace MetadataLoader.MSSQL.Contracts.ServerTypes
{
    
    
    
    
    public enum NativeTypes : byte
    {
        
        
        
        None = 0,
        
        Image = (byte) TypeIndexes.Image,
        
        Text = (byte) TypeIndexes.Text,
        
        UniqueIdentifier = (byte) TypeIndexes.UniqueIdentifier,
        
        TinyInt = (byte) TypeIndexes.TinyInt,
        
        SmallInt = (byte) TypeIndexes.SmallInt,
        
        Int = (byte) TypeIndexes.Int,
        
        SmallDateTime = (byte) TypeIndexes.SmallDateTime,
        
        Real = (byte) TypeIndexes.Real,
        
        Money = (byte) TypeIndexes.Money,
        
        DateTime = (byte) TypeIndexes.DateTime,
        
        Float = (byte) TypeIndexes.Float,
        
        SqlVariant = (byte) TypeIndexes.SqlVariant,
        
        NText = (byte) TypeIndexes.NText,
        
        Bit = (byte) TypeIndexes.Bit,
        
        Decimal = (byte) TypeIndexes.Decimal,
        
        Numeric = (byte) TypeIndexes.Numeric,
        
        SmallMoney = (byte) TypeIndexes.SmallMoney,
        
        BigInt = (byte) TypeIndexes.BigInt,
        
        Varbinary = (byte) TypeIndexes.Varbinary,
        
        Varchar = (byte) TypeIndexes.Varchar,
        
        Binary = (byte) TypeIndexes.Binary,
        
        Char = (byte) TypeIndexes.Char,
        
        TimeStamp = (byte) TypeIndexes.TimeStamp,
        
        NVarchar = (byte) TypeIndexes.NVarchar,
        
        NChar = (byte) TypeIndexes.NChar,
        
        Xml = (byte) TypeIndexes.Xml,
        
        Clr = (byte) TypeIndexes.Clr,
        
        Date = (byte) TypeIndexes.Date,
        
        Time = (byte) TypeIndexes.Time,
        
        DateTime2 = (byte) TypeIndexes.DateTime2,
        
        DateTimeOffset = (byte) TypeIndexes.DateTimeOffset,
        
        Table = (byte) TypeIndexes.Table,
    }
}