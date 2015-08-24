using ContainerStar.Contracts.Entities;
using System.Collections.Generic;
using System.IO;

namespace ContainerStar.Contracts.Managers
{
    /// <summary>
    /// </summary>
    public partial interface IPrinterManager
    {
        MemoryStream PrepareRentOrderPrintData(int id, string path, ITaxesManager taxesManager, IOrdersManager ordersManager);

        MemoryStream PrepareOfferPrintData(int id, string path, ITaxesManager taxesManager, IOrdersManager ordersManager);

        MemoryStream PrepareInvoicePrintData(int id, string path, IInvoicesManager invoicesManager, IOrdersManager ordersManager);

        MemoryStream PrepareInvoiceStornoPrintData(int id, string path, IInvoiceStornosManager invoicesManager, IOrdersManager ordersManager);

        MemoryStream PrepareReminderPrintData(IEnumerable<Invoices> invoices, string path, IInvoicesManager invoicesManager,
            ITaxesManager taxesManager, IOrdersManager ordersManager);

        MemoryStream PrepareMonthInvoicePrintData(IEnumerable<Invoices> invoices, string path, IInvoicesManager invoicesManager,
            ITaxesManager taxesManager, IOrdersManager ordersManager);

        MemoryStream PrepareTransportInvoicePrintData(int id, string path, ITransportOrdersManager transportOrdersManager,
            ITaxesManager taxesManager, IOrdersManager ordersManager);

        MemoryStream PrepareDeliveryNotePrintData(int id, string path, IOrdersManager ordersManager);

        MemoryStream PrepareBackDeliveryNotePrintData(int id, string path, IOrdersManager ordersManager);
    }
}