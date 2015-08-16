using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using CoreBase.Managers;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class CustomersManager: EntityManager<Customers, int>
        ,ICustomersManager
    {

        public CustomersManager(IContainerStarEntities context): base(context){}

    }
}
