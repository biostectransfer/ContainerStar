using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using ContainerStar.Lib.Managers.Base;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class OrdersManager: EntityManager<Orders, int>
        ,IOrdersManager
    {

        public OrdersManager(IContainerStarEntities context): base(context){}

    }
}
