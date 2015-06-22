using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using ContainerStar.Lib.Managers.Base;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class EquipmentsManager: EntityManager<Equipments, int>
        ,IEquipmentsManager
    {

        public EquipmentsManager(IContainerStarEntities context): base(context){}

    }
}
