using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using CoreBase.Managers;
using System;

namespace ContainerStar.Lib.Managers
{
    public partial class InvoiceStornosManager: EntityManager<InvoiceStornos, int>
        ,IInvoiceStornosManager
    {

        public InvoiceStornosManager(IContainerStarEntities context): base(context){}

    }
}
