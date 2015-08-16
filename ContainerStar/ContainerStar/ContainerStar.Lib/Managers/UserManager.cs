using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using CoreBase.Managers;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class UserManager: EntityManager<User, int>
        ,IUserManager
    {

        public UserManager(IContainerStarEntities context): base(context){}

    }
}
