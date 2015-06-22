using ContainerStar.API.Validation;
using ContainerStar.Contracts.Entities;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Settings
{
    /// <summary>
    ///     Model for <see cref="Equipments"/> entity
    /// </summary>
    [DataContract]
    public partial class EquipmentsModel: BaseModel
    {

        /// <summary>
        ///     Model property for <see cref="Equipments.Description"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string description{ get; set; }

    }
}
