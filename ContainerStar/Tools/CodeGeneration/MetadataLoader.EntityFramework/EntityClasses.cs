namespace MetadataLoader.EntityFramework
{
    public enum PropertyModelType
    {
        None = 0,
        Simple = 1,
        Complex = 2,
        NavigationSingle = 3,
        NavigationCollection = 4
    }

    ////NOTE: Temporary class. Need resolve interfaces by correct way
    //public class InterfaceItem
    //{
    //    public InterfaceItem(string codeName, InterfaceTemplate iface)
    //    {
    //        CodeName = codeName;
    //        Interface = iface;
    //    }

    //    public string CodeName { get; private set; }
    //    public InterfaceTemplate Interface { get; private set; }
    //}

    //public sealed class InterfaceEntityInfo : BaseEntityInfo
    //{
    //    public InterfaceEntityInfo()
    //    {
    //        Properties = new List<InterfacePropertyEntityInfo>();
    //    }

    //    public List<InterfacePropertyEntityInfo> Properties { get; private set; }
    //}

    //public class InterfacePropertyEntityInfo : BaseEntityInfo
    //{
    //    public bool HasGetter { get; set; }
    //    public bool HasSetter { get; set; }
    //}

    //public class InterfaceTemplate
    //{
    //    public InterfaceTemplate()
    //    {
    //        GenericParameters = new List<string>();
    //        Properties = new List<InterfacePropertyTemplate>();
    //        BaseInterfaces = new List<InterfaceTemplate>();
    //        BaseNames = new string[0];
    //    }

    //    public string Name { get; set; }

    //    public string CodeName
    //    {
    //        get { return IsGeneric ? string.Format("{0}<{1}>", Name, string.Join(", ", GenericParameters)) : Name; }
    //    }
    //    public string Comment { get; set; }
    //    public List<string> GenericParameters { get; private set; }
    //    public List<InterfacePropertyTemplate> Properties { get; private set; }
    //    public List<InterfaceTemplate> BaseInterfaces { get; private set; }
    //    public string[] BaseNames { get; set; }

    //    public bool IsGeneric
    //    {
    //        get { return GenericParameters.Count != 0; }
    //    }

    //    public bool IsValid()
    //    {
    //        foreach (var property in GetAllProperties())
    //        {
    //            if (!property.IsTypeDefined)
    //            {
    //                return false;
    //            }
    //        }
    //        return true;
    //    }

    //    public IEnumerable<InterfacePropertyTemplate> GetAllProperties()
    //    {
    //        foreach (var p in Properties)
    //        {
    //            yield return p;
    //        }
    //        foreach (var baseInterface in BaseInterfaces)
    //        {
    //            foreach (var p in baseInterface.GetAllProperties())
    //            {
    //                yield return p;
    //            }
    //        }
    //    }

    //    public override string ToString()
    //    {
    //        return string.Format("{0}, Properties: {1}, Base:{2} Comment:{3}", Name, Properties.Count, string.Join(",", BaseInterfaces.Select(template => template.Name)), Comment);
    //    }
    //}

    //public class InterfacePropertyTemplate
    //{
    //    public InterfaceTemplate Interface { get; set; }
    //    public string Name { get; set; }
    //    public string Comment { get; set; }
    //    public string Type { get; set; }
    //    public bool IsTypeGeneric { get; set; }

    //    public bool IsTypeDefined
    //    {
    //        get { return !string.IsNullOrWhiteSpace(Type); }
    //    }

    //    public bool CheckType(string type)
    //    {
    //        if (IsTypeDefined)
    //        {
    //            if (string.IsNullOrWhiteSpace(type))
    //            {
    //                return false;
    //            }
    //            return type.EndsWith(Type);
    //        }
    //        Type = type;
    //        return true;
    //    }

    //    public override string ToString()
    //    {
    //        return string.Format("{0}, Type: {1} Comment:{2}", Name, Type, Comment);
    //    }
    //}
}