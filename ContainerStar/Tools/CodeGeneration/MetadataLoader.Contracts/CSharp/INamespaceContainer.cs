using System.Collections.Generic;

namespace MetadataLoader.Contracts.CSharp
{
    public interface INamespaceContainer
    {
        IEnumerable<string> GetUsedNamespaces(bool includeSelf = false);
    }
}