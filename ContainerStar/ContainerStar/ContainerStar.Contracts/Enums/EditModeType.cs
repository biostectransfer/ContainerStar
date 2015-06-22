namespace ContainerStar.Contracts.Enums
{
    public enum EditModeType
    {
        /// <summary>
        ///     Readonly
        /// </summary>
        //[Display(Name = "Readonly", ResourceType = typeof(CommonObjectsResources))]
        Readonly = 0,

        /// <summary>
        ///     Read/Edit
        /// </summary>
        //[Display(Name = "Edit", ResourceType = typeof(CommonObjectsResources))]
        Edit = 1,

        /// <summary>
        ///     Read/Edit/Add
        /// </summary>
        //[Display(Name = "Add", ResourceType = typeof(CommonObjectsResources))]
        Add = 2
    }
}