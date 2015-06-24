
namespace ContainerStar.Contracts.Entities
{
    public class Numbers : IHasId<int>
    {
        public int Id { get; set; }
        public short NumberType { get; set; }
        public long CurrentNumber { get; set; }

        public static readonly string EntityTableName = "dbo.Numbers";

        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' 
            /// </summary>
            public static readonly string Id = "Id";

            public static readonly string NumberType = "NumberType";

            public static readonly string CurrentNumber = "CurrentNumber";
        }

    }
}
