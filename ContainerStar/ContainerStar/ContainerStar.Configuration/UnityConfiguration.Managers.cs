






using ContainerStar.Contracts.Managers;
using ContainerStar.Lib.Managers;
using Microsoft.Practices.Unity;

namespace ContainerStar.Configuration
{
    public static partial class UnityConfiguration
    {
        private static void InitializeContainerStar(IUnityContainer container)
        {
            container.RegisterType<IOrdersManager, OrdersManager>(new PerRequestLifetimeManager());
            container.RegisterType<IPositionsManager, PositionsManager>(new PerRequestLifetimeManager());
            container.RegisterType<IInvoicesManager, InvoicesManager>(new PerRequestLifetimeManager());
            container.RegisterType<IInvoicePositionsManager, InvoicePositionsManager>(new PerRequestLifetimeManager());
            container.RegisterType<IPermissionManager, PermissionManager>(new PerRequestLifetimeManager());
            container.RegisterType<IRoleManager, RoleManager>(new PerRequestLifetimeManager());
            container.RegisterType<IRolePermissionRspManager, RolePermissionRspManager>(new PerRequestLifetimeManager());
            container.RegisterType<IInvoiceStornosManager, InvoiceStornosManager>(new PerRequestLifetimeManager());
            container.RegisterType<IUserManager, UserManager>(new PerRequestLifetimeManager());
            container.RegisterType<IEquipmentsManager, EquipmentsManager>(new PerRequestLifetimeManager());
            container.RegisterType<IAdditionalCostsManager, AdditionalCostsManager>(new PerRequestLifetimeManager());
            container.RegisterType<ITaxesManager, TaxesManager>(new PerRequestLifetimeManager());
            container.RegisterType<ITransportProductsManager, TransportProductsManager>(new PerRequestLifetimeManager());
            container.RegisterType<ICustomersManager, CustomersManager>(new PerRequestLifetimeManager());
            container.RegisterType<ICommunicationPartnersManager, CommunicationPartnersManager>(new PerRequestLifetimeManager());
            container.RegisterType<ITransportOrdersManager, TransportOrdersManager>(new PerRequestLifetimeManager());
            container.RegisterType<IContainerTypesManager, ContainerTypesManager>(new PerRequestLifetimeManager());
            container.RegisterType<ITransportPositionsManager, TransportPositionsManager>(new PerRequestLifetimeManager());
            container.RegisterType<IContainerTypeEquipmentRspManager, ContainerTypeEquipmentRspManager>(new PerRequestLifetimeManager());
            container.RegisterType<IContainersManager, ContainersManager>(new PerRequestLifetimeManager());
            container.RegisterType<IContainerEquipmentRspManager, ContainerEquipmentRspManager>(new PerRequestLifetimeManager());
        }

    }
}
