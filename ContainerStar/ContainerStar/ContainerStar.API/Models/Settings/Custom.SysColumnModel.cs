using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ContainerStar.API.Models.Settings
{
    public partial class SysColumnModel
    {
        [DataMember]
        public string columnDescription { get; set; }
    }
}
