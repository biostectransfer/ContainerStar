using System;

namespace ContainerStar.Contracts
{
    /// <summary>
    /// Interface entity with all system fields 
    /// </summary>
    public interface ISystemFields
    {
        /// <summary>
        /// Create date entity 
        /// </summary>
        DateTime CreateDate { get; set; }
        /// <summary>
        /// Change date entity 
        /// </summary>
        DateTime ChangeDate { get; set; }
    }
}