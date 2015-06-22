using System.Runtime.Serialization;
using ContainerStar.API.Validation;

namespace ContainerStar.API.Models.Settings
{
	public interface IPasswordModel
	{
		string password { get; }
		string passwordConfirmation { get; }
	}

    [DataContract]
	public class PasswordModel : BaseModel, IPasswordModel
	{
        [DataMember]
        [Required]
		public string password { get; set; }
        [DataMember]
        [Required]
		public string passwordConfirmation { get; set; }
	}
}