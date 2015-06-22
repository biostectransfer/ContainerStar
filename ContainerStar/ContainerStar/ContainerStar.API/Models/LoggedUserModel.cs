using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ContainerStar.API.Models
{
    [DataContract]
	public class LoggedUserModel
	{
        [DataMember]
        public bool IsAuthenticated { get; set; }
        [DataMember]
        public List<string> Permissions { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Login { get; set; }
	}
}