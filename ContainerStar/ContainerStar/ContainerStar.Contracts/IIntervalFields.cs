using System;

namespace ContainerStar.Contracts
{
    /// <summary>
    /// Interface entity with FromDate and ToDate fields 
    /// </summary>
    public interface IIntervalFields
    {
        /// <summary>
        /// From date entity valid 
        /// </summary>
        DateTime? FromDate { get; set; }
        /// <summary>
        /// To date entity valid 
        /// </summary>
        DateTime? ToDate { get; set; }
    }
}