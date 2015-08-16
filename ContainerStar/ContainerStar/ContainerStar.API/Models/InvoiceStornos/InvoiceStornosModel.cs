using CoreBase.Models;
using CoreBase.Validation;
using System;
using System.Runtime.Serialization;

namespace ContainerStar.API.Models.InvoiceStornos
{
    /// <summary>
    ///     Model for Invoice Stornos
    /// </summary>
    [DataContract]
    public partial class InvoiceStornosModel: BaseModel
    {
        [Required]
        [DataMember]
        public double price{ get; set; }

        [Required]
        [DataMember]
        public int proceedsAccount { get; set; }

        [Required]
        [DataMember]
        public int invoiceId { get; set; }

        [DataMember]
        public string freeText { get; set; }
    }
}
