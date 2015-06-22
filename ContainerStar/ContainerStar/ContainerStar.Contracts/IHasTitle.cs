namespace ContainerStar.Contracts
{
    /// <summary>
    /// Interface entity with name
    /// </summary>
    public interface IHasTitle<TId>
    {
        /// <summary>
        /// Entity id  
        /// </summary>
        TId Id { get; set; }

        /// <summary>
        /// Entity name
        /// </summary>
        string EntityTitle { get; }
    }
}
