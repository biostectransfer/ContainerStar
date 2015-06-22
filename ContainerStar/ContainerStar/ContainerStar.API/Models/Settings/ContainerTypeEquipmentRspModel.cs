using ContainerStar.API.Validation;
using ContainerStar.Contracts.Entities;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Settings
{
    /// <summary>
    ///     Model for <see cref="ContainerTypeEquipmentRsp"/> entity
    /// </summary>
    [DataContract]
    public partial class ContainerTypeEquipmentRspModel: BaseModel
    {

        /// <summary>
        ///     Model property for <see cref="ContainerTypeEquipmentRsp.ContainerTypeId"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int containerTypeId{ get; set; }
        /// <summary>
        ///     Model property for <see cref="ContainerTypeEquipmentRsp.EquipmentId"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int equipmentId{ get; set; }
        /// <summary>
        ///     Model property for <see cref="ContainerTypeEquipmentRsp.Amount"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int amount{ get; set; }

    }
}
