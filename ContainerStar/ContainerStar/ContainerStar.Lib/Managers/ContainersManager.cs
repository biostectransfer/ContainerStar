using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using ContainerStar.Lib.Managers.Base;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class ContainersManager: EntityManager<Containers, int>
        ,IContainersManager
    {

        public ContainersManager(IContainerStarEntities context): base(context){}

    }
}
