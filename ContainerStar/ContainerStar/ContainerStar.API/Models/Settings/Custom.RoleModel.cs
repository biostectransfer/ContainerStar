using System.Collections.Generic;
using System.Runtime.Serialization;
using ContainerStar.API.Validation;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Settings
{
	public partial class RoleModel
	{
        [DataMember]
        public IEnumerable<int> permissions { get; set; }
	}
}