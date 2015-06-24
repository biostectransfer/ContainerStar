using ContainerStar.Contracts.Entities;
using System.IO;

namespace ContainerStar.Contracts.Managers
{
    /// <summary>
    /// </summary>
    public partial interface IOrdersManager
    {
        Stream PrepareRentOrderPrintData(int id, string path);
    }
}