using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using ContainerStar.Lib.Managers.Base;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class PermissionManager: EntityManager<Permission, int>
        ,IPermissionManager
    {

        public PermissionManager(IContainerStarEntities context): base(context){}

    }
}
