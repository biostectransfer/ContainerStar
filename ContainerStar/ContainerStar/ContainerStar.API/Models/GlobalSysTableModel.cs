using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ContainerStar.API.Models
{
    [DataContract]
    public class GlobalSysTableModel
	{
        [DataMember]
        public Dictionary<string, SysTableWithColumnsModel> TableNames { get; set; }
	}

    [DataContract]
    public class SysTableWithColumnsModel
    {
        [DataMember]
        public int EditMode { get; set; }
        [DataMember]
        public List<string> ReadOnlyColumns { get; set; }
    }
}