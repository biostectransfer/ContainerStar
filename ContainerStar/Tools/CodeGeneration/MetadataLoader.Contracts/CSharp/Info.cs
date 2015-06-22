using System.Collections.Generic;

namespace MetadataLoader.Contracts.CSharp
{
    public abstract class Info : INamespaceContainer
    {
        public virtual string Name { get; set; }
        public abstract IEnumerable<string> GetUsedNamespaces(bool includeSelf = false);
    }
}