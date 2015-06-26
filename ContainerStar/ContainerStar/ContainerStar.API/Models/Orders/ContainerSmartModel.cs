using ContainerStar.API.Models.Settings;
using System;

namespace ContainerStar.API.Models.Orders
{
    public class ContainerSmartModel : ContainersModel
    {
        public DateTime? fromDate { get; set; }


        public DateTime? toDate { get; set; }
       
    }
}
