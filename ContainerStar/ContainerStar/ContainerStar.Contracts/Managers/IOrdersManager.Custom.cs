using ContainerStar.Contracts.Entities;
using System.Collections.Generic;
using System.IO;

namespace ContainerStar.Contracts.Managers
{
    /// <summary>
    /// </summary>
    public partial interface IOrdersManager
    {
        MemoryStream PrepareRentOrderPrintData(int id, string path, ITaxesManager taxesManager);

        MemoryStream PrepareOfferPrintData(int id, string path, ITaxesManager taxesManager);

        MemoryStream PrepareInvoicePrintData(int id, string path, IInvoicesManager invoicesManager);

        MemoryStream PrepareInvoiceStornoPrintData(int id, string path, IInvoiceStornosManager invoicesManager);

        MemoryStream PrepareReminderPrintData(IEnumerable<Invoices> invoices, string path, IInvoicesManager invoicesManager, ITaxesManager taxesManager);

        MemoryStream PrepareMonthInvoicePrintData(IEnumerable<Invoices> invoices, string path, IInvoicesManager invoicesManager, ITaxesManager taxesManager);

        MemoryStream PrepareTransportInvoicePrintData(int id, string path, ITransportOrdersManager transportOrdersManager, ITaxesManager taxesManager);
    }
}