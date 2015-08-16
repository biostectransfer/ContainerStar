using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using CoreBase.Managers;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class ContainerEquipmentRspManager: EntityManager<ContainerEquipmentRsp, int>
        ,IContainerEquipmentRspManager
    {

        public ContainerEquipmentRspManager(IContainerStarEntities context): base(context){}

    }
}
