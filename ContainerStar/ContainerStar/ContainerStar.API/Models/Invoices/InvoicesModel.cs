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

        [Required]
        [DataMember]
        public DateTime? payDate{ get; set; }

        [DataMember]
        public string customerName { get; set; }

        [DataMember]
        public string communicationPartnerName { get; set; }

    }
}
