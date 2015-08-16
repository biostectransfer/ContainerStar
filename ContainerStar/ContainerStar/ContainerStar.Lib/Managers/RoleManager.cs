using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using CoreBase.Managers;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class RoleManager: EntityManager<Role, int>
        ,IRoleManager
    {

        public RoleManager(IContainerStarEntities context): base(context){}

    }
}
