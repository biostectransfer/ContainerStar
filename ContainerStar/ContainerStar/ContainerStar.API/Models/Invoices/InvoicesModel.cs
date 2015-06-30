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

        /// <summary>
        ///     Model property for <see cref="Invoices.InvoiceNumber"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string invoiceNumber{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Invoices.PayDate"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public DateTime? payDate{ get; set; }

        [DataMember]
        public string customerName { get; set; }

        [DataMember]
        public string communicationPartnerName { get; set; }

    }
}
