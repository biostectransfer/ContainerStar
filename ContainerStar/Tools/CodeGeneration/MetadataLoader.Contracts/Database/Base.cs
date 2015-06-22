namespace MetadataLoader.Contracts.Database
{
    public abstract class Base<T> : IBaseObject<T>
        where T : IContent, new()
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        protected Base()
        {
            Content = new T();
        }
        #endregion
        #region	Public properties
        public string Name { get; set; }
        public T Content { get; private set; }
        IContent IBaseObject.Content
        {
            get { return Content; }
        }
        public string Description { get; set; }
        #endregion
    }
}