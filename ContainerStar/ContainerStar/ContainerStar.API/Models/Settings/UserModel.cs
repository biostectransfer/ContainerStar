using ContainerStar.API.Validation;
using ContainerStar.Contracts.Entities;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Settings
{
    /// <summary>
    ///     Model for <see cref="User"/> entity
    /// </summary>
    [DataContract]
    public partial class UserModel: BaseModel
    {

        /// <summary>
        ///     Model property for <see cref="User.RoleId"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int? roleId{ get; set; }
        /// <summary>
        ///     Model property for <see cref="User.Login"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string login{ get; set; }
        /// <summary>
        ///     Model property for <see cref="User.Name"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string name{ get; set; }
        /// <summary>
        ///     Model property for <see cref="User.Password"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string password{ get; set; }

    }
}
