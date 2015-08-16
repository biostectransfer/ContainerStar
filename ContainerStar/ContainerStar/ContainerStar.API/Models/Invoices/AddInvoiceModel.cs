using CoreBase.Models;
using CoreBase.Validation;
using System;
using System.Runtime.Serialization;

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
        
        [DataMember]
        public bool isSell { get; set; }
    }
}
