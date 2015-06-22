using ContainerStar.API.Validation;
using ContainerStar.Contracts.Entities;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Settings
{
    /// <summary>
    ///     Model for <see cref="Containers"/> entity
    /// </summary>
    [DataContract]
    public partial class ContainersModel: BaseModel
    {

        /// <summary>
        ///     Model property for <see cref="Containers.Number"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string number{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Containers.ContainerTypeId"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int containerTypeId{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Containers.Length"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int length{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Containers.Width"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int width{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Containers.Height"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int height{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Containers.Color"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string color{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Containers.Price"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public double price{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Containers.ProceedsAccount"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int proceedsAccount{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Containers.IsVirtual"/> entity
        /// </summary>
        [DataMember]
        public bool isVirtual{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Containers.ManufactureDate"/> entity
        /// </summary>
        [DataMember]
        public DateTime? manufactureDate{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Containers.BoughtFrom"/> entity
        /// </summary>
        [DataMember]
        public string boughtFrom{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Containers.BoughtPrice"/> entity
        /// </summary>
        [DataMember]
        public double? boughtPrice{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Containers.Comment"/> entity
        /// </summary>
        [DataMember]
        public string comment{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Containers.SellPrice"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public double sellPrice{ get; set; }

    }
}
