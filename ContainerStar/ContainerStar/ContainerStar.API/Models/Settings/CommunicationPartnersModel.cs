using ContainerStar.Contracts.Entities;
using CoreBase.Models;
using CoreBase.Validation;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Settings
{
    /// <summary>
    ///     Model for <see cref="CommunicationPartners"/> entity
    /// </summary>
    [DataContract]
    public partial class CommunicationPartnersModel: BaseModel
    {

        /// <summary>
        ///     Model property for <see cref="CommunicationPartners.Name"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string name{ get; set; }
        /// <summary>
        ///     Model property for <see cref="CommunicationPartners.FirstName"/> entity
        /// </summary>
        [DataMember]
        public string firstName{ get; set; }
        /// <summary>
        ///     Model property for <see cref="CommunicationPartners.CustomerId"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int customerId{ get; set; }
        /// <summary>
        ///     Model property for <see cref="CommunicationPartners.Phone"/> entity
        /// </summary>
        [DataMember]
        public string phone{ get; set; }
        /// <summary>
        ///     Model property for <see cref="CommunicationPartners.Mobile"/> entity
        /// </summary>
        [DataMember]
        public string mobile{ get; set; }
        /// <summary>
        ///     Model property for <see cref="CommunicationPartners.Fax"/> entity
        /// </summary>
        [DataMember]
        public string fax{ get; set; }
        /// <summary>
        ///     Model property for <see cref="CommunicationPartners.Email"/> entity
        /// </summary>
        [DataMember]
        public string email{ get; set; }

    }
}
