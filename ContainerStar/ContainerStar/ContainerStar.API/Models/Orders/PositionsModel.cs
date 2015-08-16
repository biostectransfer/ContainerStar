using CoreBase.Models;
using CoreBase.Validation;
using System;
using System.Runtime.Serialization;

namespace ContainerStar.API.Models
{
    /// <summary>
    ///     Model for <see cref="PositionsModel"/> entity
    /// </summary>
    [DataContract]
    public partial class PositionsModel : BaseModel
    {
        [Required]
        [DataMember]
        public int orderId{ get; set; }

        [Required]
        [DataMember]
        public bool isSellOrder { get; set; }

        [DataMember]
        public int? containerId { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public int? additionalCostId { get; set; }
        
        [Required]
        [DataMember]
        public double price { get; set; }

        [Required]
        [DataMember]
        public int amount { get; set; }

        [Required]
        [DataMember]
        public int paymentType { get; set; }

        [DataMember]
        public DateTime? fromDate { get; set; }

        [DataMember]
        public DateTime? toDate { get; set; }

        [Required]
        [DataMember]
        public bool isMain { get; set; }

        [DataMember]
        public bool isOffer { get; set; }
    }
}
