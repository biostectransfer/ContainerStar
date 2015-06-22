using System.Runtime.Serialization;
using ContainerStar.API.Validation;

namespace ContainerStar.API.Models.Settings
{
    public partial class MasterDataMonitorableInfoMasterDataNotificationsRspModel
	{
        [DataMember]
        public string monitorableInfoObject { get; set; }
        
        [DataMember]
        public string monitorableInfoTypeText { get; set; }
	}
}