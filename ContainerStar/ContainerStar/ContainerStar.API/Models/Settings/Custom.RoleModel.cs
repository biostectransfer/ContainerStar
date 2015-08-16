using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ContainerStar.API.Models.Settings
{
	public partial class RoleModel
	{
        [DataMember]
        public IEnumerable<int> permissions { get; set; }
	}
}