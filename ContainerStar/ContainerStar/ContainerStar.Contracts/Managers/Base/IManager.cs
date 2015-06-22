namespace ContainerStar.Contracts.Managers.Base
{
    /// <summary>
    /// 
    /// </summary>
	public interface IManager
	{
        /// <summary>
        /// 
        /// </summary>
		IEntities DataContext
		{
			get;
			set;
		}

        /// <summary>
        /// 
        /// </summary>
		void SaveChanges();
	}    
}
