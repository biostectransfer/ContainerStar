using System.Diagnostics.Contracts;
using MetadataLoader.Contracts.Database;

namespace MetadataLoader.EntityFramework
{
    public sealed class ContentEntityHandler<TIn, TContent, T> : IEntityHandler<TIn, T>
        where TIn : IBaseObject<TContent>
    {
        #region	Private fields
        private readonly IEntityHandler<TContent, T>[] _handlers;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public ContentEntityHandler(params IEntityHandler<TContent, T>[] handlers)
        {
            Contract.Requires(handlers != null);
            Contract.Requires(handlers.Length > 0);

            _handlers = handlers;
        }
        #endregion
        #region	Public methods
        public void Run(TIn input, T obj)
        {
            foreach (var handler in _handlers)
            {
                handler.Run(input.Content, obj);
            }
        }
        #endregion
    }
}