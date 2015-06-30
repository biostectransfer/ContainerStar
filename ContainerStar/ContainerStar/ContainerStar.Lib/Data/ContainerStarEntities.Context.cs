






using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Lib.Data;
using System.Data.Entity;
using System.Linq;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Database context for for ContainerStar
    /// </summary>
    public partial class ContainerStarEntities: IContainerStarEntities
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(OrdersMapping.Instance);
            modelBuilder.Configurations.Add(PositionsMapping.Instance);
            modelBuilder.Configurations.Add(InvoicesMapping.Instance);
            modelBuilder.Configurations.Add(InvoicePositionsMapping.Instance);
            modelBuilder.Configurations.Add(PermissionMapping.Instance);
            modelBuilder.Configurations.Add(RoleMapping.Instance);
            modelBuilder.Configurations.Add(RolePermissionRspMapping.Instance);
            modelBuilder.Configurations.Add(UserMapping.Instance);
            modelBuilder.Configurations.Add(EquipmentsMapping.Instance);
            modelBuilder.Configurations.Add(AdditionalCostsMapping.Instance);
            modelBuilder.Configurations.Add(TaxesMapping.Instance);
            modelBuilder.Configurations.Add(TransportProductsMapping.Instance);
            modelBuilder.Configurations.Add(CustomersMapping.Instance);
            modelBuilder.Configurations.Add(CommunicationPartnersMapping.Instance);
            modelBuilder.Configurations.Add(ContainerTypesMapping.Instance);
            modelBuilder.Configurations.Add(ContainerTypeEquipmentRspMapping.Instance);
            modelBuilder.Configurations.Add(ContainersMapping.Instance);
            modelBuilder.Configurations.Add(ContainerEquipmentRspMapping.Instance);
        }

        /// <summary>
        ///     Set of <see cref="Orders"/> entities from table dbo.Orders
        /// </summary>
        public IQueryable<Orders> Orders{ get; set; }
        /// <summary>
        ///     Set of <see cref="Positions"/> entities from table dbo.Positions
        /// </summary>
        public IQueryable<Positions> Positions{ get; set; }
        /// <summary>
        ///     Set of <see cref="Invoices"/> entities from table dbo.Invoices
        /// </summary>
        public IQueryable<Invoices> Invoices{ get; set; }
        /// <summary>
        ///     Set of <see cref="InvoicePositions"/> entities from table dbo.InvoicePositions
        /// </summary>
        public IQueryable<InvoicePositions> InvoicePositions{ get; set; }
        /// <summary>
        ///     Set of <see cref="Permission"/> entities from table dbo.Permission
        /// </summary>
        public IQueryable<Permission> Permission{ get; set; }
        /// <summary>
        ///     Set of <see cref="Role"/> entities from table dbo.Role
        /// </summary>
        public IQueryable<Role> Role{ get; set; }
        /// <summary>
        ///     Set of <see cref="RolePermissionRsp"/> entities from table dbo.Role_Permission_Rsp
        /// </summary>
        public IQueryable<RolePermissionRsp> RolePermissionRsp{ get; set; }
        /// <summary>
        ///     Set of <see cref="User"/> entities from table dbo.User
        /// </summary>
        public IQueryable<User> User{ get; set; }
        /// <summary>
        ///     Set of <see cref="Equipments"/> entities from table dbo.Equipments
        /// </summary>
        public IQueryable<Equipments> Equipments{ get; set; }
        /// <summary>
        ///     Set of <see cref="AdditionalCosts"/> entities from table dbo.AdditionalCosts
        /// </summary>
        public IQueryable<AdditionalCosts> AdditionalCosts{ get; set; }
        /// <summary>
        ///     Set of <see cref="Taxes"/> entities from table dbo.Taxes
        /// </summary>
        public IQueryable<Taxes> Taxes{ get; set; }
        /// <summary>
        ///     Set of <see cref="TransportProducts"/> entities from table dbo.TransportProducts
        /// </summary>
        public IQueryable<TransportProducts> TransportProducts{ get; set; }
        /// <summary>
        ///     Set of <see cref="Customers"/> entities from table dbo.Customers
        /// </summary>
        public IQueryable<Customers> Customers{ get; set; }
        /// <summary>
        ///     Set of <see cref="CommunicationPartners"/> entities from table dbo.CommunicationPartners
        /// </summary>
        public IQueryable<CommunicationPartners> CommunicationPartners{ get; set; }
        /// <summary>
        ///     Set of <see cref="ContainerTypes"/> entities from table dbo.ContainerTypes
        /// </summary>
        public IQueryable<ContainerTypes> ContainerTypes{ get; set; }
        /// <summary>
        ///     Set of <see cref="ContainerTypeEquipmentRsp"/> entities from table dbo.ContainerType_Equipment_Rsp
        /// </summary>
        public IQueryable<ContainerTypeEquipmentRsp> ContainerTypeEquipmentRsp{ get; set; }
        /// <summary>
        ///     Set of <see cref="Containers"/> entities from table dbo.Containers
        /// </summary>
        public IQueryable<Containers> Containers{ get; set; }
        /// <summary>
        ///     Set of <see cref="ContainerEquipmentRsp"/> entities from table dbo.Container_Equipment_Rsp
        /// </summary>
        public IQueryable<ContainerEquipmentRsp> ContainerEquipmentRsp{ get; set; }
    }
}
