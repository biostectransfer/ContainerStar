using ContainerStar.Contracts;
using System;
using System.Collections.Generic;

namespace ContainerStar.Contracts.Entities
{
    public partial class Customers: IHasId<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.Customers";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="Customers.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'Number' for property <see cref="Customers.Number"/>
            /// </summary>
            public static readonly string Number = "Number";
            /// <summary>
            /// Column name 'Name' for property <see cref="Customers.Name"/>
            /// </summary>
            public static readonly string Name = "Name";
            /// <summary>
            /// Column name 'Street' for property <see cref="Customers.Street"/>
            /// </summary>
            public static readonly string Street = "Street";
            /// <summary>
            /// Column name 'ZIP' for property <see cref="Customers.Zip"/>
            /// </summary>
            public static readonly string Zip = "ZIP";
            /// <summary>
            /// Column name 'City' for property <see cref="Customers.City"/>
            /// </summary>
            public static readonly string City = "City";
            /// <summary>
            /// Column name 'Country' for property <see cref="Customers.Country"/>
            /// </summary>
            public static readonly string Country = "Country";
            /// <summary>
            /// Column name 'Phone' for property <see cref="Customers.Phone"/>
            /// </summary>
            public static readonly string Phone = "Phone";
            /// <summary>
            /// Column name 'Mobile' for property <see cref="Customers.Mobile"/>
            /// </summary>
            public static readonly string Mobile = "Mobile";
            /// <summary>
            /// Column name 'Fax' for property <see cref="Customers.Fax"/>
            /// </summary>
            public static readonly string Fax = "Fax";
            /// <summary>
            /// Column name 'Email' for property <see cref="Customers.Email"/>
            /// </summary>
            public static readonly string Email = "Email";
            /// <summary>
            /// Column name 'Comment' for property <see cref="Customers.Comment"/>
            /// </summary>
            public static readonly string Comment = "Comment";
            /// <summary>
            /// Column name 'IBAN' for property <see cref="Customers.Iban"/>
            /// </summary>
            public static readonly string Iban = "IBAN";
            /// <summary>
            /// Column name 'BIC' for property <see cref="Customers.Bic"/>
            /// </summary>
            public static readonly string Bic = "BIC";
            /// <summary>
            /// Column name 'WithTaxes' for property <see cref="Customers.WithTaxes"/>
            /// </summary>
            public static readonly string WithTaxes = "WithTaxes";
            /// <summary>
            /// Column name 'AutoDebitEntry' for property <see cref="Customers.AutoDebitEntry"/>
            /// </summary>
            public static readonly string AutoDebitEntry = "AutoDebitEntry";
            /// <summary>
            /// Column name 'AutoBill' for property <see cref="Customers.AutoBill"/>
            /// </summary>
            public static readonly string AutoBill = "AutoBill";
            /// <summary>
            /// Column name 'Discount' for property <see cref="Customers.Discount"/>
            /// </summary>
            public static readonly string Discount = "Discount";
            /// <summary>
            /// Column name 'UstId' for property <see cref="Customers.UstId"/>
            /// </summary>
            public static readonly string UstId = "UstId";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="Customers.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="Customers.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="Customers.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
            /// <summary>
            /// Column name 'Bank' for property <see cref="Customers.Bank"/>
            /// </summary>
            public static readonly string Bank = "Bank";
            /// <summary>
            /// Column name 'AccountNumber' for property <see cref="Customers.AccountNumber"/>
            /// </summary>
            public static readonly string AccountNumber = "AccountNumber";
            /// <summary>
            /// Column name 'BLZ' for property <see cref="Customers.Blz"/>
            /// </summary>
            public static readonly string Blz = "BLZ";
            /// <summary>
            /// Column name 'IsProspectiveCustomer' for property <see cref="Customers.IsProspectiveCustomer"/>
            /// </summary>
            public static readonly string IsProspectiveCustomer = "IsProspectiveCustomer";
          
        }
        #endregion
        public int Id{ get; set; }
        public int Number{ get; set; }
        public string Name{ get; set; }
        public string Street{ get; set; }
        public string Zip{ get; set; }
        public string City{ get; set; }
        public string Country{ get; set; }
        public string Phone{ get; set; }
        public string Mobile{ get; set; }
        public string Fax{ get; set; }
        public string Email{ get; set; }
        public string Comment{ get; set; }
        public string Iban{ get; set; }
        public string Bic{ get; set; }
        public bool WithTaxes{ get; set; }
        public bool AutoDebitEntry{ get; set; }
        public bool AutoBill{ get; set; }
        public double? Discount{ get; set; }
        public string UstId{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public string Bank{ get; set; }
        public string AccountNumber{ get; set; }
        public string Blz{ get; set; }
        public bool IsProspectiveCustomer{ get; set; }
        public virtual ICollection<Orders> Orders{ get; set; }
        public virtual ICollection<CommunicationPartners> CommunicationPartners{ get; set; }
        public virtual ICollection<TransportOrders> TransportOrders{ get; set; }
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
        public Customers ShallowCopy()
        {
            return new Customers {
                       Number = Number,
                       Name = Name,
                       Street = Street,
                       Zip = Zip,
                       City = City,
                       Country = Country,
                       Phone = Phone,
                       Mobile = Mobile,
                       Fax = Fax,
                       Email = Email,
                       Comment = Comment,
                       Iban = Iban,
                       Bic = Bic,
                       WithTaxes = WithTaxes,
                       AutoDebitEntry = AutoDebitEntry,
                       AutoBill = AutoBill,
                       Discount = Discount,
                       UstId = UstId,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
                       Bank = Bank,
                       AccountNumber = AccountNumber,
                       Blz = Blz,
                       IsProspectiveCustomer = IsProspectiveCustomer,
        	           };
        }
    }
}
