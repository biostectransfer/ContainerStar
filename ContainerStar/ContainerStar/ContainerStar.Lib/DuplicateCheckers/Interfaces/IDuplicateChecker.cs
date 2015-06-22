namespace ContainerStar.Lib.DuplicateCheckers.Interfaces
{
    public interface IDuplicateChecker
    {
        bool HasDuplicate(object entity);
        string GetWorkingTypeName();
        string[] BusinessKeys { get; }
    }
}