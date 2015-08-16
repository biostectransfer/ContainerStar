using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using CoreBase.Managers;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class AdditionalCostsManager: EntityManager<AdditionalCosts, int>
        ,IAdditionalCostsManager
    {

        public AdditionalCostsManager(IContainerStarEntities context): base(context){}

    }
}
