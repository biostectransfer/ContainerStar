using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using CoreBase.Managers;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class TransportOrdersManager: EntityManager<TransportOrders, int>
        ,ITransportOrdersManager
    {

        public TransportOrdersManager(IContainerStarEntities context): base(context){}

    }
}
