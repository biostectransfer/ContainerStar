using System;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models
{
    /// <summary>
    ///     Interface model with FromDate and ToDate fields
    /// </summary>
    public interface IModelIntervalFields
    {
        /// <summary>
        ///     From date entity valid
        /// </summary>
        DateTime? fromDate { get; set; }
        /// <summary>
        ///     To date entity valid
        /// </summary>
        DateTime? toDate { get; set; }
    }
}