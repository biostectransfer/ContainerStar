using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using CoreBase.Managers;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class TransportProductsManager: EntityManager<TransportProducts, int>
        ,ITransportProductsManager
    {

        public TransportProductsManager(IContainerStarEntities context): base(context){}

    }
}
