using ContainerStar.Contracts.Entities;
using System.Collections.Generic;
using System.IO;

namespace ContainerStar.Contracts.Managers
{
    /// <summary>
    /// </summary>
    public partial interface IOrdersManager
    {
        Stream PrepareRentOrderPrintData(int id, string path, ITaxesManager taxesManager);

        Stream PrepareOfferPrintData(int id, string path, ITaxesManager taxesManager);

        Stream PrepareInvoicePrintData(int id, string path, IInvoicesManager invoicesManager, ITaxesManager taxesManager);

        Stream PrepareReminderPrintData(int id, string path, IInvoicesManager invoicesManager, ITaxesManager taxesManager);

        Stream PrepareMonthInvoicePrintData(IEnumerable<Invoices> invoices, string path, IInvoicesManager invoicesManager, ITaxesManager taxesManager);
    }
}