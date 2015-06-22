using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ContainerStar.API.Models.Settings
{
    public partial class SysTableModel
    {
        [DataMember]
        public string tableDescription { get; set; }
    }
}
