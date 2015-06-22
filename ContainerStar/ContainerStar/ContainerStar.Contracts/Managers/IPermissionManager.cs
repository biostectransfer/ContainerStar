using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers.Base;
using System;

namespace ContainerStar.Contracts.Managers
{
    public partial interface IPermissionManager: IEntityManager<Permission, int>
    {
    }
}
