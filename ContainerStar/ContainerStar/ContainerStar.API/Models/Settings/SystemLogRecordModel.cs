using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Settings
{
    [DataContract]
    public class SystemLogRecordModel : BaseModel
	{
        [DataMember]
        public string userLogin { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public DateTime date { get; set; }
	}
}