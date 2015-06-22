using ContainerStar.API.Validation;
using ContainerStar.Contracts.Entities;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Settings
{
    /// <summary>
    ///     Model for <see cref="ContainerEquipmentRsp"/> entity
    /// </summary>
    [DataContract]
    public partial class ContainerEquipmentRspModel: BaseModel
    {

        /// <summary>
        ///     Model property for <see cref="ContainerEquipmentRsp.ContainerId"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int containerId{ get; set; }
        /// <summary>
        ///     Model property for <see cref="ContainerEquipmentRsp.EquipmentId"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int equipmentId{ get; set; }
        /// <summary>
        ///     Model property for <see cref="ContainerEquipmentRsp.Amount"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int amount{ get; set; }

    }
}
