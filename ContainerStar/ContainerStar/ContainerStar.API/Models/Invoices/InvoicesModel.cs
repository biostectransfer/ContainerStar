using ContainerStar.API.Validation;
using ContainerStar.Contracts.Entities;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Invoices
{
    /// <summary>
    ///     Model for <see cref="Invoices"/> entity
    /// </summary>
    [DataContract]
    public partial class InvoicesModel: BaseModel
    {
        [Required]
        [DataMember]
        public int orderId { get; set; }
        
        [DataMember]
        public string invoiceNumber{ get; set; }

        [DataMember]
        public DateTime? payDate{ get; set; }

        [DataMember]
        public string customerName { get; set; }

        [DataMember]
        public string customerAddress { get; set; }
        
        [DataMember]
        public string rentOrderNumber { get; set; }

        [DataMember]
        public string communicationPartnerName { get; set; }

        [DataMember]
        public bool withTaxes { get; set; }

        [DataMember]
        public double discount { get; set; }

        [DataMember]
        public double taxValue { get; set; }

        [DataMember]
        public double? manualPrice { get; set; }

        [DataMember]
        public double totalPrice { get; set; }
    }
}
