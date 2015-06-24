
namespace ContainerStar.Contracts.Services
{
    public interface IUniqueNumberProvider
    {
        string GetNextOrderNumber();

        string GetNextRentOrderNumber(string preffix);

        string GetNextInvoiceNumber();
    }
}
