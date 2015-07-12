using ContainerStar.API.Controllers;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace ContainerStar.API.Controllers
{
    /// <summary>
    ///     MasterDataViewCollectionControllerFactory class
    /// </summary>
    public class MasterDataViewCollectionControllerFactory: ViewCollectionControllerFactoryBase
    {
        public void GetViewCollections(IDependencyResolver resolver, CollectionTypesModel model, Dictionary<string, IEnumerable<object>> result)
        {
            if (model.Permission)
            	result.Add("Permission", GetViewCollection<Permission, int, IPermissionManager>(
            		(IPermissionManager)resolver.GetService(typeof(IPermissionManager))));

            if (model.Role)
            	result.Add("Role", GetViewCollection<Role, int, IRoleManager>(
            		(IRoleManager)resolver.GetService(typeof(IRoleManager))));

            if (model.Equipments)
            	result.Add("Equipments", GetViewCollection<Equipments, int, IEquipmentsManager>(
            		(IEquipmentsManager)resolver.GetService(typeof(IEquipmentsManager))));

            if (model.TransportProducts)
            	result.Add("TransportProducts", GetViewCollection<TransportProducts, int, ITransportProductsManager>(
            		(ITransportProductsManager)resolver.GetService(typeof(ITransportProductsManager))));

            if (model.ContainerTypes)
            	result.Add("ContainerTypes", GetViewCollection<ContainerTypes, int, IContainerTypesManager>(
            		(IContainerTypesManager)resolver.GetService(typeof(IContainerTypesManager))));

        }
    }
}
