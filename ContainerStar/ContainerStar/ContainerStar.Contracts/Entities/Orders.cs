using ContainerStar.Contracts;
using System;
using System.Collections.Generic;

namespace ContainerStar.Contracts.Entities
{
    public partial class Orders: IHasId<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.Orders";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="Orders.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'CustomerId' for property <see cref="Orders.CustomerId"/>
            /// </summary>
            public static readonly string CustomerId = "CustomerId";
            /// <summary>
            /// Column name 'CommunicationPartnerId' for property <see cref="Orders.CommunicationPartnerId"/>
            /// </summary>
            public static readonly string CommunicationPartnerId = "CommunicationPartnerId";
            /// <summary>
            /// Column name 'IsOffer' for property <see cref="Orders.IsOffer"/>
            /// </summary>
            public static readonly string IsOffer = "IsOffer";
            /// <summary>
            /// Column name 'DeliveryPlace' for property <see cref="Orders.DeliveryPlace"/>
            /// </summary>
            public static readonly string DeliveryPlace = "DeliveryPlace";
            /// <summary>
            /// Column name 'Street' for property <see cref="Orders.Street"/>
            /// </summary>
            public static readonly string Street = "Street";
            /// <summary>
            /// Column name 'ZIP' for property <see cref="Orders.Zip"/>
            /// </summary>
            public static readonly string Zip = "ZIP";
            /// <summary>
            /// Column name 'City' for property <see cref="Orders.City"/>
            /// </summary>
            public static readonly string City = "City";
            /// <summary>
            /// Column name 'Comment' for property <see cref="Orders.Comment"/>
            /// </summary>
            public static readonly string Comment = "Comment";
            /// <summary>
            /// Column name 'OrderDate' for property <see cref="Orders.OrderDate"/>
            /// </summary>
            public static readonly string OrderDate = "OrderDate";
            /// <summary>
            /// Column name 'OrderedFrom' for property <see cref="Orders.OrderedFrom"/>
            /// </summary>
            public static readonly string OrderedFrom = "OrderedFrom";
            /// <summary>
            /// Column name 'OrderNumber' for property <see cref="Orders.OrderNumber"/>
            /// </summary>
            public static readonly string OrderNumber = "OrderNumber";
            /// <summary>
            /// Column name 'RentOrderNumber' for property <see cref="Orders.RentOrderNumber"/>
            /// </summary>
            public static readonly string RentOrderNumber = "RentOrderNumber";
            /// <summary>
            /// Column name 'RentFromDate' for property <see cref="Orders.RentFromDate"/>
            /// </summary>
            public static readonly string RentFromDate = "RentFromDate";
            /// <summary>
            /// Column name 'RentToDate' for property <see cref="Orders.RentToDate"/>
            /// </summary>
            public static readonly string RentToDate = "RentToDate";
            /// <summary>
            /// Column name 'AutoBill' for property <see cref="Orders.AutoBill"/>
            /// </summary>
            public static readonly string AutoBill = "AutoBill";
            /// <summary>
            /// Column name 'Discount' for property <see cref="Orders.Discount"/>
            /// </summary>
            public static readonly string Discount = "Discount";
            /// <summary>
            /// Column name 'BillTillDate' for property <see cref="Orders.BillTillDate"/>
            /// </summary>
            public static readonly string BillTillDate = "BillTillDate";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="Orders.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="Orders.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="Orders.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
            /// <summary>
            /// Column name 'CustomerOrderNumber' for property <see cref="Orders.CustomerOrderNumber"/>
            /// </summary>
            public static readonly string CustomerOrderNumber = "CustomerOrderNumber";
            /// <summary>
            /// Column name 'AutoProlongation' for property <see cref="Orders.AutoProlongation"/>
            /// </summary>
            public static readonly string AutoProlongation = "AutoProlongation";
            /// <summary>
            /// Column name 'Status' for property <see cref="Orders.Status"/>
            /// </summary>
            public static readonly string Status = "Status";
          
        }
        #endregion
        public int Id{ get; set; }
        public int CustomerId{ get; set; }
        public int? CommunicationPartnerId{ get; set; }
        public bool IsOffer{ get; set; }
        public string DeliveryPlace{ get; set; }
        public string Street{ get; set; }
        public string Zip{ get; set; }
        public string City{ get; set; }
        public string Comment{ get; set; }
        public DateTime? OrderDate{ get; set; }
        public string OrderedFrom{ get; set; }
        public string OrderNumber{ get; set; }
        public string RentOrderNumber{ get; set; }
        public DateTime? RentFromDate{ get; set; }
        public DateTime? RentToDate{ get; set; }
        public bool AutoBill{ get; set; }
        public double? Discount{ get; set; }
        public DateTime? BillTillDate{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public string CustomerOrderNumber{ get; set; }
        public bool AutoProlongation{ get; set; }
        public int Status{ get; set; }
        public virtual Customers Customers{ get; set; }
        public virtual CommunicationPartners CommunicationPartners{ get; set; }
        public virtual ICollection<Positions> Positions{ get; set; }
        public virtual ICollection<Invoices> Invoices{ get; set; }
        public bool HasCustomers
        {
            get { return !ReferenceEquals(Customers, null); }
        }
        public bool HasCommunicationPartners
        {
            get { return !ReferenceEquals(CommunicationPartners, null); }
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
        public Orders ShallowCopy()
        {
            return new Orders {
                       CustomerId = CustomerId,
                       CommunicationPartnerId = CommunicationPartnerId,
                       IsOffer = IsOffer,
                       DeliveryPlace = DeliveryPlace,
                       Street = Street,
                       Zip = Zip,
                       City = City,
                       Comment = Comment,
                       OrderDate = OrderDate,
                       OrderedFrom = OrderedFrom,
                       OrderNumber = OrderNumber,
                       RentOrderNumber = RentOrderNumber,
                       RentFromDate = RentFromDate,
                       RentToDate = RentToDate,
                       AutoBill = AutoBill,
                       Discount = Discount,
                       BillTillDate = BillTillDate,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
                       CustomerOrderNumber = CustomerOrderNumber,
                       AutoProlongation = AutoProlongation,
                       Status = Status,
        	           };
        }
    }
}
