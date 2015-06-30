using ContainerStar.Contracts;
using System;
using System.Collections.Generic;

namespace ContainerStar.Contracts.Entities
{
    public partial class Invoices: IHasId<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.Invoices";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="Invoices.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'OrderId' for property <see cref="Invoices.OrderId"/>
            /// </summary>
            public static readonly string OrderId = "OrderId";
            /// <summary>
            /// Column name 'InvoiceNumber' for property <see cref="Invoices.InvoiceNumber"/>
            /// </summary>
            public static readonly string InvoiceNumber = "InvoiceNumber";
            /// <summary>
            /// Column name 'PayDate' for property <see cref="Invoices.PayDate"/>
            /// </summary>
            public static readonly string PayDate = "PayDate";
            /// <summary>
            /// Column name 'WithTaxes' for property <see cref="Invoices.WithTaxes"/>
            /// </summary>
            public static readonly string WithTaxes = "WithTaxes";
            /// <summary>
            /// Column name 'ManualPrice' for property <see cref="Invoices.ManualPrice"/>
            /// </summary>
            public static readonly string ManualPrice = "ManualPrice";
            /// <summary>
            /// Column name 'TaxValue' for property <see cref="Invoices.TaxValue"/>
            /// </summary>
            public static readonly string TaxValue = "TaxValue";
            /// <summary>
            /// Column name 'Discount' for property <see cref="Invoices.Discount"/>
            /// </summary>
            public static readonly string Discount = "Discount";
            /// <summary>
            /// Column name 'BillTillDate' for property <see cref="Invoices.BillTillDate"/>
            /// </summary>
            public static readonly string BillTillDate = "BillTillDate";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="Invoices.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="Invoices.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="Invoices.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
          
        }
        #endregion
        public int Id{ get; set; }
        public int OrderId{ get; set; }
        public string InvoiceNumber{ get; set; }
        public DateTime? PayDate{ get; set; }
        public bool WithTaxes{ get; set; }
        public double? ManualPrice{ get; set; }
        public double TaxValue{ get; set; }
        public double Discount{ get; set; }
        public DateTime? BillTillDate{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public virtual Orders Orders{ get; set; }
        public virtual ICollection<InvoicePositions> InvoicePositions{ get; set; }
        public bool HasOrders
        {
            get { return !ReferenceEquals(Orders, null); }
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
        public Invoices ShallowCopy()
        {
            return new Invoices {
                       OrderId = OrderId,
                       InvoiceNumber = InvoiceNumber,
                       PayDate = PayDate,
                       WithTaxes = WithTaxes,
                       ManualPrice = ManualPrice,
                       TaxValue = TaxValue,
                       Discount = Discount,
                       BillTillDate = BillTillDate,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
        	           };
        }
    }
}
