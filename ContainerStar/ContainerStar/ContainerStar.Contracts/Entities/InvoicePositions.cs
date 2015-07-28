using ContainerStar.Contracts;
using System;

namespace ContainerStar.Contracts.Entities
{
    public partial class InvoicePositions: IHasId<int>
        ,IIntervalFields
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.InvoicePositions";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="InvoicePositions.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'InvoiceId' for property <see cref="InvoicePositions.InvoiceId"/>
            /// </summary>
            public static readonly string InvoiceId = "InvoiceId";
            /// <summary>
            /// Column name 'PositionId' for property <see cref="InvoicePositions.PositionId"/>
            /// </summary>
            public static readonly string PositionId = "PositionId";
            /// <summary>
            /// Column name 'Price' for property <see cref="InvoicePositions.Price"/>
            /// </summary>
            public static readonly string Price = "Price";
            /// <summary>
            /// Column name 'FromDate' for property <see cref="InvoicePositions.FromDate"/>
            /// </summary>
            public static readonly string FromDate = "FromDate";
            /// <summary>
            /// Column name 'ToDate' for property <see cref="InvoicePositions.ToDate"/>
            /// </summary>
            public static readonly string ToDate = "ToDate";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="InvoicePositions.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="InvoicePositions.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="InvoicePositions.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
            /// <summary>
            /// Column name 'Amount' for property <see cref="InvoicePositions.Amount"/>
            /// </summary>
            public static readonly string Amount = "Amount";
            /// <summary>
            /// Column name 'PaymentType' for property <see cref="InvoicePositions.PaymentType"/>
            /// </summary>
            public static readonly string PaymentType = "PaymentType";
          
        }
        #endregion
        public int Id{ get; set; }
        public int InvoiceId{ get; set; }
        public int PositionId{ get; set; }
        public double Price{ get; set; }
        public DateTime FromDate{ get; set; }
        public DateTime ToDate{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public int Amount{ get; set; }
        public int PaymentType{ get; set; }
        public virtual Invoices Invoices{ get; set; }
        public virtual Positions Positions{ get; set; }
        public bool HasInvoices
        {
            get { return !ReferenceEquals(Invoices, null); }
        }
        public bool HasPositions
        {
            get { return !ReferenceEquals(Positions, null); }
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
        public InvoicePositions ShallowCopy()
        {
            return new InvoicePositions {
                       InvoiceId = InvoiceId,
                       PositionId = PositionId,
                       Price = Price,
                       FromDate = FromDate,
                       ToDate = ToDate,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
                       Amount = Amount,
                       PaymentType = PaymentType,
        	           };
        }
    }
}
