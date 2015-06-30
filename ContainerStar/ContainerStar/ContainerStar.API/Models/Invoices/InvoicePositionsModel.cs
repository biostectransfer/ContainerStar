using ContainerStar.API.Validation;
using ContainerStar.Contracts.Entities;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Invoices
{
    /// <summary>
    ///     Model for <see cref="InvoicePositions"/> entity
    /// </summary>
    [DataContract]
    public partial class InvoicePositionsModel: BaseModel
    {

        /// <summary>
        ///     Model property for <see cref="InvoicePositions.Price"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public double price{ get; set; }

    }
}
