using CoreBase.Models;
using CoreBase.Validation;
using System;
using System.Runtime.Serialization;

namespace ContainerStar.API.Models.Invoices
{
    /// <summary>
    ///     Model for <see cref="InvoicePositions"/> entity
    /// </summary>
    [DataContract]
    public partial class InvoicePositionsModel: BaseModel
    {
        [Required]
        [DataMember]
        public double price{ get; set; }

        [Required]
        [DataMember]
        public double totalPrice { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public int amount { get; set; }

        [DataMember]
        public DateTime? fromDate { get; set; }

        [DataMember]
        public DateTime? toDate { get; set; }

        [DataMember]
        public bool isCointainerPosition { get; set; }

        [Required]
        [DataMember]
        public int paymentType { get; set; }
    }
}
