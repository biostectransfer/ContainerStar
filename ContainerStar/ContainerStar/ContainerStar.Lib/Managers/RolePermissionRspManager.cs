using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using ContainerStar.Lib.Managers.Base;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class RolePermissionRspManager: EntityManager<RolePermissionRsp, int>
        ,IRolePermissionRspManager
    {

        public RolePermissionRspManager(IContainerStarEntities context): base(context){}

    }
}
