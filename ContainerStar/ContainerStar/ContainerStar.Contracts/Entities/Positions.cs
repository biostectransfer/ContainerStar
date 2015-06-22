using ContainerStar.Contracts;
using System;

namespace ContainerStar.Contracts.Entities
{
    public partial class Positions: IHasId<int>
        ,IIntervalFields
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.Positions";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="Positions.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'OrderId' for property <see cref="Positions.OrderId"/>
            /// </summary>
            public static readonly string OrderId = "OrderId";
            /// <summary>
            /// Column name 'IsSellOrder' for property <see cref="Positions.IsSellOrder"/>
            /// </summary>
            public static readonly string IsSellOrder = "IsSellOrder";
            /// <summary>
            /// Column name 'ContainerId' for property <see cref="Positions.ContainerId"/>
            /// </summary>
            public static readonly string ContainerId = "ContainerId";
            /// <summary>
            /// Column name 'AdditionalCostId' for property <see cref="Positions.AdditionalCostId"/>
            /// </summary>
            public static readonly string AdditionalCostId = "AdditionalCostId";
            /// <summary>
            /// Column name 'Price' for property <see cref="Positions.Price"/>
            /// </summary>
            public static readonly string Price = "Price";
            /// <summary>
            /// Column name 'FromDate' for property <see cref="Positions.FromDate"/>
            /// </summary>
            public static readonly string FromDate = "FromDate";
            /// <summary>
            /// Column name 'ToDate' for property <see cref="Positions.ToDate"/>
            /// </summary>
            public static readonly string ToDate = "ToDate";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="Positions.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="Positions.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="Positions.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
          
        }
        #endregion
        public int Id{ get; set; }
        public int OrderId{ get; set; }
        public bool IsSellOrder{ get; set; }
        public int? ContainerId{ get; set; }
        public int? AdditionalCostId{ get; set; }
        public double Price{ get; set; }
        public DateTime FromDate{ get; set; }
        public DateTime ToDate{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public virtual Orders Orders{ get; set; }
        public virtual Containers Containers{ get; set; }
        public virtual AdditionalCosts AdditionalCosts{ get; set; }
        public bool HasOrders
        {
            get { return !ReferenceEquals(Orders, null); }
        }
        public bool HasContainers
        {
            get { return !ReferenceEquals(Containers, null); }
        }
        public bool HasAdditionalCosts
        {
            get { return !ReferenceEquals(AdditionalCosts, null); }
        }
        DateTime? IIntervalFields.FromDate
        {
            get { return FromDate; }
            set { if(value.HasValue)FromDate = value.Value; else throw new ArgumentNullException("value"); }
        }
        DateTime? IIntervalFields.ToDate
        {
            get { return ToDate; }
            set { if(value.HasValue)ToDate = value.Value; else throw new ArgumentNullException("value"); }
        }
        DateTime ISystemFields.CreateDate
        {
            get { return CreateDate; }
            set { CreateDate = value; }
        }
        DateTime ISystemFields.ChangeDate
        {
            get { return ChangeDate; }
            set { ChangeDate = value; }
        }
                
        
        /// <summary>
        /// Shallow copy of object. Exclude navigation properties and PK properties
        /// </summary>
        public Positions ShallowCopy()
        {
            return new Positions {
                       OrderId = OrderId,
                       IsSellOrder = IsSellOrder,
                       ContainerId = ContainerId,
                       AdditionalCostId = AdditionalCostId,
                       Price = Price,
                       FromDate = FromDate,
                       ToDate = ToDate,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
        	           };
        }
    }
}
