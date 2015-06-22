using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers.Base;
using System;
using System.IO;

namespace ContainerStar.Contracts.Managers
{
    public partial interface IOrdersManager: IEntityManager<Orders, int>
    {
        Stream PrepareRentOrderPrintData(int id, string path);
    }
}
