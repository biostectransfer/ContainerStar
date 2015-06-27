using System.Runtime.Serialization;
using ContainerStar.API.Models.Settings;
using System;

namespace ContainerStar.API.Models.Orders
{
    public class ContainerSmartModel : ContainersModel
    {
        [DataMember]
        public DateTime fromDate { get; set; }

        [DataMember]
        public DateTime toDate { get; set; }
       
    }
}
