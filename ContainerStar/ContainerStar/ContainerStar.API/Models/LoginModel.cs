using System.Runtime.Serialization;

namespace ContainerStar.API.Models
{
    [DataContract]
	public class LoginModel
	{
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public bool RememberMe { get; set; }
	}
}