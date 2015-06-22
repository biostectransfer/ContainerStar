using System;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models
{
    /// <summary>
    ///     Interface model with system fields
    /// </summary>
    public interface ISystemModelFields
    {
        /// <summary>
        ///     Create date entity
        /// </summary>
        DateTime createDate { get; set; }
        /// <summary>
        ///     Change date entity
        /// </summary>
        DateTime changeDate { get; set; }
    }
}