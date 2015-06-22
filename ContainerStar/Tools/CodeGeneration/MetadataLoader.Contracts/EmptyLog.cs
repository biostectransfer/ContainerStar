namespace MetadataLoader.Contracts
{
    public sealed class EmptyLog : ILog
    {
        public static readonly ILog Instance=new EmptyLog();
        private EmptyLog()
        {
        }
        public void SendWarning(string message)
        {
        }
        public void SendError(string message)
        {
        }
    }
}