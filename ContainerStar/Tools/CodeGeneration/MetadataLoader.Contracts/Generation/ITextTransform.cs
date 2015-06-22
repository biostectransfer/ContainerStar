namespace MetadataLoader.Contracts.Generation
{
    public interface ITextTransform
    {
        void Write(string value, params object[] args);
        void WriteLine(string value, params object[] args);
        void PushIndent(string indent);
        void PopIndent();
    }
}