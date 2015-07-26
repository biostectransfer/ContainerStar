using ContainerStar.Contracts;
using System;
using System.Collections.Generic;

namespace ContainerStar.Contracts.Entities
{
    public partial class TransportOrders: IHasId<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.TransportOrders";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="TransportOrders.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'CustomerId' for property <see cref="TransportOrders.CustomerId"/>
            /// </summary>
            public static readonly string CustomerId = "CustomerId";
            /// <summary>
            /// Column name 'CommunicationPartnerId' for property <see cref="TransportOrders.CommunicationPartnerId"/>
            /// </summary>
            public static readonly string CommunicationPartnerId = "CommunicationPartnerId";
            /// <summary>
            /// Column name 'IsOffer' for property <see cref="TransportOrders.IsOffer"/>
            /// </summary>
            public static readonly string IsOffer = "IsOffer";
            /// <summary>
            /// Column name 'DeliveryPlace' for property <see cref="TransportOrders.DeliveryPlace"/>
            /// </summary>
            public static readonly string DeliveryPlace = "DeliveryPlace";
            /// <summary>
            /// Column name 'Street' for property <see cref="TransportOrders.Street"/>
            /// </summary>
            public static readonly string Street = "Street";
            /// <summary>
            /// Column name 'ZIP' for property <see cref="TransportOrders.Zip"/>
            /// </summary>
            public static readonly string Zip = "ZIP";
            /// <summary>
            /// Column name 'City' for property <see cref="TransportOrders.City"/>
            /// </summary>
            public static readonly string City = "City";
            /// <summary>
            /// Column name 'Comment' for property <see cref="TransportOrders.Comment"/>
            /// </summary>
            public static readonly string Comment = "Comment";
            /// <summary>
            /// Column name 'OrderDate' for property <see cref="TransportOrders.OrderDate"/>
            /// </summary>
            public static readonly string OrderDate = "OrderDate";
            /// <summary>
            /// Column name 'OrderedFrom' for property <see cref="TransportOrders.OrderedFrom"/>
            /// </summary>
            public static readonly string OrderedFrom = "OrderedFrom";
            /// <summary>
            /// Column name 'OrderNumber' for property <see cref="TransportOrders.OrderNumber"/>
            /// </summary>
            public static readonly string OrderNumber = "OrderNumber";
            /// <summary>
            /// Column name 'CustomerOrderNumber' for property <see cref="TransportOrders.CustomerOrderNumber"/>
            /// </summary>
            public static readonly string CustomerOrderNumber = "CustomerOrderNumber";
            /// <summary>
            /// Column name 'Discount' for property <see cref="TransportOrders.Discount"/>
            /// </summary>
            public static readonly string Discount = "Discount";
            /// <summary>
            /// Column name 'BillTillDate' for property <see cref="TransportOrders.BillTillDate"/>
            /// </summary>
            public static readonly string BillTillDate = "BillTillDate";
            /// <summary>
            /// Column name 'Status' for property <see cref="TransportOrders.Status"/>
            /// </summary>
            public static readonly string Status = "Status";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="TransportOrders.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="TransportOrders.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="TransportOrders.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
          
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
        public string CustomerOrderNumber{ get; set; }
        public double? Discount{ get; set; }
        public DateTime? BillTillDate{ get; set; }
        public int Status{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public virtual Customers Customers{ get; set; }
        public virtual CommunicationPartners CommunicationPartners{ get; set; }
        public virtual ICollection<TransportPositions> TransportPositions{ get; set; }
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
        public TransportOrders ShallowCopy()
        {
            return new TransportOrders {
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
                       CustomerOrderNumber = CustomerOrderNumber,
                       Discount = Discount,
                       BillTillDate = BillTillDate,
                       Status = Status,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
        	           };
        }
    }
}
