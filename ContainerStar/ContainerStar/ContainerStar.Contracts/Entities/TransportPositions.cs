using ContainerStar.Contracts;
using CoreBase.Entities;
using System;

namespace ContainerStar.Contracts.Entities
{
    public partial class TransportPositions: IHasId<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.TransportPositions";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="TransportPositions.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'TransportOrderId' for property <see cref="TransportPositions.TransportOrderId"/>
            /// </summary>
            public static readonly string TransportOrderId = "TransportOrderId";
            /// <summary>
            /// Column name 'TransportProductId' for property <see cref="TransportPositions.TransportProductId"/>
            /// </summary>
            public static readonly string TransportProductId = "TransportProductId";
            /// <summary>
            /// Column name 'Price' for property <see cref="TransportPositions.Price"/>
            /// </summary>
            public static readonly string Price = "Price";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="TransportPositions.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="TransportPositions.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="TransportPositions.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
            /// <summary>
            /// Column name 'Amount' for property <see cref="TransportPositions.Amount"/>
            /// </summary>
            public static readonly string Amount = "Amount";
          
        }
        #endregion
        public int Id{ get; set; }
        public int TransportOrderId{ get; set; }
        public int TransportProductId{ get; set; }
        public double Price{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public int Amount{ get; set; }
        public virtual TransportOrders TransportOrders{ get; set; }
        public virtual TransportProducts TransportProducts{ get; set; }
        public bool HasTransportOrders
        {
            get { return !ReferenceEquals(TransportOrders, null); }
        }
        public bool HasTransportProducts
        {
            get { return !ReferenceEquals(TransportProducts, null); }
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
        public TransportPositions ShallowCopy()
        {
            return new TransportPositions {
                       TransportOrderId = TransportOrderId,
                       TransportProductId = TransportProductId,
                       Price = Price,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
                       Amount = Amount,
        	           };
        }
    }
}
