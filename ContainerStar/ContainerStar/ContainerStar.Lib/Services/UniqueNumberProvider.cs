
using System.Linq;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Services;
using ContainerStar.Lib.Managers.Base;
using System.Collections.Generic;


namespace ContainerStar.Lib.Services
{
    public class UniqueNumberProvider : Manager, IUniqueNumberProvider
    {
        public UniqueNumberProvider(IContainerStarEntities context)
            : base(context)
        {

        }

        public string GetNextOrderNumber()
        {
            return GetNextNumber(UniqueNumberType.OrderNumber).ToString();
        }

        public string GetNextTransportOrderNumber()
        {
            return GetNextNumber(UniqueNumberType.TransportOrderNumber).ToString();
        }
        
        public string GetNextRentOrderNumber(string preffix)
        {
            var temp = GetNextNumber(UniqueNumberType.RentOrderNumber);
            return string.Format("{0}{1}", preffix, temp);
        }

        public string GetNextInvoiceNumber()
        {
            return GetNextNumber(UniqueNumberType.InvoiceNumber).ToString();

        }
  
        private long GetNextNumber(UniqueNumberType type)
        {
            lock (this)
            {
                var temp = DataContext.GetSet<Numbers>().FirstOrDefault(o => o.NumberType == (short)type);
                if (temp == null)
                {
                    throw new KeyNotFoundException(string.Format("Data is not provided by the DB: {0}", type.ToString()));
                }
                temp.CurrentNumber++;
                SaveChanges();
                return temp.CurrentNumber;
            }
        }
    }
}
