using ContainerStar.API.Validation;
using ContainerStar.Contracts.Entities;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Settings
{
    /// <summary>
    ///     Model for <see cref="Role"/> entity
    /// </summary>
    [DataContract]
    public partial class RoleModel: BaseModel
    {

        /// <summary>
        ///     Model property for <see cref="Role.Name"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string name{ get; set; }

    }
}
