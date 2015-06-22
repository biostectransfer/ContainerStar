using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using ContainerStar.Lib.Managers.Base;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class PositionsManager: EntityManager<Positions, int>
        ,IPositionsManager
    {

        public PositionsManager(IContainerStarEntities context): base(context){}

    }
}
