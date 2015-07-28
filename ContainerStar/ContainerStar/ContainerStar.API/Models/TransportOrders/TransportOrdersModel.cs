using ContainerStar.API.Validation;
using ContainerStar.Contracts.Entities;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models
{
    /// <summary>
    ///     Model for <see cref="TransportOrders"/> entity
    /// </summary>
    [DataContract]
    public partial class TransportOrdersModel : BaseModel
    {
        [DataMember]
        public string customerName { get; set; }

        /// <summary>
        ///     Model property for <see cref="Orders.CustomerId"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int customerId{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Orders.CommunicationPartnerId"/> entity
        /// </summary>
        [DataMember]
        public int? communicationPartnerId{ get; set; }

        [DataMember]
        public string communicationPartnerTitle { get; set; }
              
        /// <summary>
        ///     Model property for <see cref="Orders.DeliveryPlace"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string deliveryPlace{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Orders.Street"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string street{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Orders.Zip"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string zip{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Orders.City"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string city{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Orders.Comment"/> entity
        /// </summary>
        [DataMember]
        public string comment{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Orders.OrderDate"/> entity
        /// </summary>
        [DataMember]
        public DateTime? orderDate{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Orders.OrderedFrom"/> entity
        /// </summary>
        [DataMember]
        public string orderedFrom{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Orders.OrderNumber"/> entity
        /// </summary>
        [DataMember]
        public string customerOrderNumber { get; set; }
        /// <summary>
        ///     Model property for <see cref="Orders.OrderNumber"/> entity
        /// </summary>
        [DataMember]
        public string orderNumber{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Orders.RentOrderNumber"/> entity
        /// </summary>
        /// <summary>
        ///     Model property for <see cref="Orders.Discount"/> entity
        /// </summary>
        [DataMember]
        public double? discount{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Orders.BillTillDate"/> entity
        /// </summary>
        [DataMember]
        public DateTime? billTillDate{ get; set; }


        /// <summary>
        ///     Model property for <see cref="Customers.Number"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int customerNumber { get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Name"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string newCustomerName { get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Street"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string customerStreet { get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Zip"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string customerZip { get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.City"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string customerCity { get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Phone"/> entity
        /// </summary>
        [DataMember]
        public string customerPhone { get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Fax"/> entity
        /// </summary>
        [DataMember]
        public string customerFax { get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Email"/> entity
        /// </summary>
        [DataMember]
        public string customerEmail { get; set; }

        [DataMember]
        public int customerSelectType { get; set; }

        [DataMember]
        public bool isOffer { get; set; }

        [DataMember]
        public int status { get; set; }
    }
}
