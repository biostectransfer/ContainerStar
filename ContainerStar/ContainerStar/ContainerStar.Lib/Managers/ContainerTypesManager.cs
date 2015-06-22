using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using ContainerStar.Lib.Managers.Base;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class ContainerTypesManager: EntityManager<ContainerTypes, int>
        ,IContainerTypesManager
    {

        public ContainerTypesManager(IContainerStarEntities context): base(context){}

    }
}
