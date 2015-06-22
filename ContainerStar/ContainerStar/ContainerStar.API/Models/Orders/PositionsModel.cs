using ContainerStar.API.Validation;
using ContainerStar.Contracts.Entities;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

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
        public int? additionalCostId { get; set; }

        [Required]
        [DataMember]
        public double price { get; set; }

        [Required]
        [DataMember]
        public DateTime fromDate { get; set; }

        [Required]
        [DataMember]
        public DateTime toDate { get; set; }
    }
}
