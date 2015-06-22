namespace MetadataLoader.Contracts
{
    public interface ILog
    {
        void SendWarning(string message);
        void SendError(string message);
    }
}