using ContainerStar.Contracts;
using CoreBase.Entities;
using System;

namespace ContainerStar.Contracts.Entities
{
    public partial class InvoiceStornos: IHasId<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.InvoiceStornos";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="InvoiceStornos.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'InvoiceId' for property <see cref="InvoiceStornos.InvoiceId"/>
            /// </summary>
            public static readonly string InvoiceId = "InvoiceId";
            /// <summary>
            /// Column name 'ProceedsAccount' for property <see cref="InvoiceStornos.ProceedsAccount"/>
            /// </summary>
            public static readonly string ProceedsAccount = "ProceedsAccount";
            /// <summary>
            /// Column name 'Price' for property <see cref="InvoiceStornos.Price"/>
            /// </summary>
            public static readonly string Price = "Price";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="InvoiceStornos.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="InvoiceStornos.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="InvoiceStornos.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
            /// <summary>
            /// Column name 'DateVExportDate' for property <see cref="InvoiceStornos.DateVExportDate"/>
            /// </summary>
            public static readonly string DateVExportDate = "DateVExportDate";
            /// <summary>
            /// Column name 'DateVExportFile' for property <see cref="InvoiceStornos.DateVExportFile"/>
            /// </summary>
            public static readonly string DateVExportFile = "DateVExportFile";
            /// <summary>
            /// Column name 'FreeText' for property <see cref="InvoiceStornos.FreeText"/>
            /// </summary>
            public static readonly string FreeText = "FreeText";
          
        }
        #endregion
        public int Id{ get; set; }
        public int InvoiceId{ get; set; }
        public int ProceedsAccount{ get; set; }
        public double Price{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public DateTime? DateVExportDate{ get; set; }
        public string DateVExportFile{ get; set; }
        public string FreeText{ get; set; }
        public virtual Invoices Invoices{ get; set; }
        public bool HasInvoices
        {
            get { return !ReferenceEquals(Invoices, null); }
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
        public InvoiceStornos ShallowCopy()
        {
            return new InvoiceStornos {
                       InvoiceId = InvoiceId,
                       ProceedsAccount = ProceedsAccount,
                       Price = Price,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
                       DateVExportDate = DateVExportDate,
                       DateVExportFile = DateVExportFile,
                       FreeText = FreeText,
        	           };
        }
    }
}
