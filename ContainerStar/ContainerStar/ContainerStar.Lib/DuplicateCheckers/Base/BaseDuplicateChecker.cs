namespace ContainerStar.Lib.DuplicateCheckers.Base
{
    public abstract class BaseDuplicateChecker<TEntity> where TEntity : class
    {
        public bool HasDuplicate(object entity)
        {
            return HasDuplicate(entity as TEntity);
        }

        public string GetWorkingTypeName()
        {
            return typeof (TEntity).Name;
        }

        protected abstract bool HasDuplicate(TEntity entity);

        public abstract string[] BusinessKeys { get; }
    }
}
