using ContainerStar.API.Validation;
using ContainerStar.Contracts.Entities;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Settings
{
    /// <summary>
    ///     Model for <see cref="AdditionalCosts"/> entity
    /// </summary>
    [DataContract]
    public partial class AdditionalCostsModel: BaseModel
    {

        /// <summary>
        ///     Model property for <see cref="AdditionalCosts.Name"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string name{ get; set; }
        /// <summary>
        ///     Model property for <see cref="AdditionalCosts.Description"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string description{ get; set; }
        /// <summary>
        ///     Model property for <see cref="AdditionalCosts.Price"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public double price{ get; set; }
        /// <summary>
        ///     Model property for <see cref="AdditionalCosts.Automatic"/> entity
        /// </summary>
        [DataMember]
        public bool automatic{ get; set; }
        /// <summary>
        ///     Model property for <see cref="AdditionalCosts.IncludeInFirstBill"/> entity
        /// </summary>
        [DataMember]
        public bool includeInFirstBill{ get; set; }
        /// <summary>
        ///     Model property for <see cref="AdditionalCosts.ProceedsAccount"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int proceedsAccount{ get; set; }

    }
}
