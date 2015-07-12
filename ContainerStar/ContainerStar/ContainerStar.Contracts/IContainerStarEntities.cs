






using ContainerStar.Contracts.Entities;
using System.Linq;

namespace ContainerStar.Contracts
{
    /// <summary>
    ///     Interface for ContainerStar context
    /// </summary>
    public partial interface IContainerStarEntities
    {
        /// <summary>
        ///     Set of <see cref="Orders"/> entities from table dbo.Orders
        /// </summary>
        IQueryable<Orders> Orders{get;}
        /// <summary>
        ///     Set of <see cref="Positions"/> entities from table dbo.Positions
        /// </summary>
        IQueryable<Positions> Positions{get;}
        /// <summary>
        ///     Set of <see cref="Invoices"/> entities from table dbo.Invoices
        /// </summary>
        IQueryable<Invoices> Invoices{get;}
        /// <summary>
        ///     Set of <see cref="InvoicePositions"/> entities from table dbo.InvoicePositions
        /// </summary>
        IQueryable<InvoicePositions> InvoicePositions{get;}
        /// <summary>
        ///     Set of <see cref="Permission"/> entities from table dbo.Permission
        /// </summary>
        IQueryable<Permission> Permission{get;}
        /// <summary>
        ///     Set of <see cref="Role"/> entities from table dbo.Role
        /// </summary>
        IQueryable<Role> Role{get;}
        /// <summary>
        ///     Set of <see cref="RolePermissionRsp"/> entities from table dbo.Role_Permission_Rsp
        /// </summary>
        IQueryable<RolePermissionRsp> RolePermissionRsp{get;}
        /// <summary>
        ///     Set of <see cref="InvoiceStornos"/> entities from table dbo.InvoiceStornos
        /// </summary>
        IQueryable<InvoiceStornos> InvoiceStornos{get;}
        /// <summary>
        ///     Set of <see cref="User"/> entities from table dbo.User
        /// </summary>
        IQueryable<User> User{get;}
        /// <summary>
        ///     Set of <see cref="Equipments"/> entities from table dbo.Equipments
        /// </summary>
        IQueryable<Equipments> Equipments{get;}
        /// <summary>
        ///     Set of <see cref="AdditionalCosts"/> entities from table dbo.AdditionalCosts
        /// </summary>
        IQueryable<AdditionalCosts> AdditionalCosts{get;}
        /// <summary>
        ///     Set of <see cref="Taxes"/> entities from table dbo.Taxes
        /// </summary>
        IQueryable<Taxes> Taxes{get;}
        /// <summary>
        ///     Set of <see cref="TransportProducts"/> entities from table dbo.TransportProducts
        /// </summary>
        IQueryable<TransportProducts> TransportProducts{get;}
        /// <summary>
        ///     Set of <see cref="Customers"/> entities from table dbo.Customers
        /// </summary>
        IQueryable<Customers> Customers{get;}
        /// <summary>
        ///     Set of <see cref="CommunicationPartners"/> entities from table dbo.CommunicationPartners
        /// </summary>
        IQueryable<CommunicationPartners> CommunicationPartners{get;}
        /// <summary>
        ///     Set of <see cref="TransportOrders"/> entities from table dbo.TransportOrders
        /// </summary>
        IQueryable<TransportOrders> TransportOrders{get;}
        /// <summary>
        ///     Set of <see cref="ContainerTypes"/> entities from table dbo.ContainerTypes
        /// </summary>
        IQueryable<ContainerTypes> ContainerTypes{get;}
        /// <summary>
        ///     Set of <see cref="TransportPositions"/> entities from table dbo.TransportPositions
        /// </summary>
        IQueryable<TransportPositions> TransportPositions{get;}
        /// <summary>
        ///     Set of <see cref="ContainerTypeEquipmentRsp"/> entities from table dbo.ContainerType_Equipment_Rsp
        /// </summary>
        IQueryable<ContainerTypeEquipmentRsp> ContainerTypeEquipmentRsp{get;}
        /// <summary>
        ///     Set of <see cref="Containers"/> entities from table dbo.Containers
        /// </summary>
        IQueryable<Containers> Containers{get;}
        /// <summary>
        ///     Set of <see cref="ContainerEquipmentRsp"/> entities from table dbo.Container_Equipment_Rsp
        /// </summary>
        IQueryable<ContainerEquipmentRsp> ContainerEquipmentRsp{get;}
    }
}
