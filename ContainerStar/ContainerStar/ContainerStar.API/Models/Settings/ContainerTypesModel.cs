using ContainerStar.Contracts.Entities;
using CoreBase.Models;
using CoreBase.Validation;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Settings
{
    /// <summary>
    ///     Model for <see cref="ContainerTypes"/> entity
    /// </summary>
    [DataContract]
    public partial class ContainerTypesModel: BaseModel
    {

        /// <summary>
        ///     Model property for <see cref="ContainerTypes.Name"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string name{ get; set; }
        /// <summary>
        ///     Model property for <see cref="ContainerTypes.Comment"/> entity
        /// </summary>
        [DataMember]
        public string comment{ get; set; }
        /// <summary>
        ///     Model property for <see cref="ContainerTypes.DispositionRelevant"/> entity
        /// </summary>
        [DataMember]
        public bool dispositionRelevant{ get; set; }

    }
}
