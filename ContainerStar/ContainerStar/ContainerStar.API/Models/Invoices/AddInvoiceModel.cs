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
    public partial class AddInvoiceModel : BaseModel
    {
        [Required]
        [DataMember]
        public int orderId { get; set; }
        
        [DataMember]
        public bool isMonthlyInvoice { get; set; }
    }
}
