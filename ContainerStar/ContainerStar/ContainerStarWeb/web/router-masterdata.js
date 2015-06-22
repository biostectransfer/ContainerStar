define([
    'base-router'
], function (BaseRouter) {
	'use strict';
    
	var factory = {
	    
	    getAllMasterDataRoutes: function(baseRouter)
	    {
	        var routes = {
				'Permissions': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/Permissions', false),
	            'Permissions/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddPermission', 'models/Settings/Permission', false),
	            'Permissions/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddPermission', 'models/Settings/Permission', false),
				'Roles': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/Roles', { Permission: true, Role: true, }),
	            'Roles/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddRole', 'models/Settings/Role', { Permission: true, Role: true, }),
	            'Roles/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddRole', 'models/Settings/Role', { Permission: true, Role: true, }),
				'RolePermissionRsps': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/RolePermissionRsps', { Permission: true, Role: true, }),
	            'RolePermissionRsps/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddRolePermissionRsp', 'models/Settings/RolePermissionRsp', { Permission: true, Role: true, }),
	            'RolePermissionRsps/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddRolePermissionRsp', 'models/Settings/RolePermissionRsp', { Permission: true, Role: true, }),
				'Users': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/Users', { Role: true, }),
	            'Users/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddUser', 'models/Settings/User', { Role: true, }),
	            'Users/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddUser', 'models/Settings/User', { Role: true, }),
				'Equipments': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/Equipments', false),
	            'Equipments/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddEquipments', 'models/Settings/Equipments', false),
	            'Equipments/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddEquipments', 'models/Settings/Equipments', false),
				'AdditionalCosts': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/AdditionalCosts', false),
	            'AdditionalCosts/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddAdditionalCosts', 'models/Settings/AdditionalCosts', false),
	            'AdditionalCosts/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddAdditionalCosts', 'models/Settings/AdditionalCosts', false),
				'Taxes': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/Taxes', false),
	            'Taxes/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddTaxes', 'models/Settings/Taxes', false),
	            'Taxes/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddTaxes', 'models/Settings/Taxes', false),
				'TransportProducts': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/TransportProducts', false),
	            'TransportProducts/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddTransportProducts', 'models/Settings/TransportProducts', false),
	            'TransportProducts/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddTransportProducts', 'models/Settings/TransportProducts', false),
				'Customers': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/Customers', false),
	            'Customers/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddCustomers', 'models/Settings/Customers', false),
	            'Customers/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddCustomers', 'models/Settings/Customers', false),
				'CommunicationPartners': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/CommunicationPartners', false),
	            'CommunicationPartners/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddCommunicationPartners', 'models/Settings/CommunicationPartners', false),
	            'CommunicationPartners/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddCommunicationPartners', 'models/Settings/CommunicationPartners', false),
				'ContainerTypes': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/ContainerTypes', { Equipments: true, }),
	            'ContainerTypes/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddContainerTypes', 'models/Settings/ContainerTypes', { Equipments: true, }),
	            'ContainerTypes/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddContainerTypes', 'models/Settings/ContainerTypes', { Equipments: true, }),
				'ContainerTypeEquipmentRsps': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/ContainerTypeEquipmentRsps', { Equipments: true, }),
	            'ContainerTypeEquipmentRsps/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddContainerTypeEquipmentRsp', 'models/Settings/ContainerTypeEquipmentRsp', { Equipments: true, }),
	            'ContainerTypeEquipmentRsps/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddContainerTypeEquipmentRsp', 'models/Settings/ContainerTypeEquipmentRsp', { Equipments: true, }),
				'Containers': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/Containers', { Equipments: true, ContainerTypes: true, }),
	            'Containers/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddContainers', 'models/Settings/Containers', { Equipments: true, ContainerTypes: true, }),
	            'Containers/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddContainers', 'models/Settings/Containers', { Equipments: true, ContainerTypes: true, }),
				'ContainerEquipmentRsps': _.partial(BaseRouter.showView, baseRouter, 'l!t!Settings/ContainerEquipmentRsps', { Equipments: true, }),
	            'ContainerEquipmentRsps/create': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddContainerEquipmentRsp', 'models/Settings/ContainerEquipmentRsp', { Equipments: true, }),
	            'ContainerEquipmentRsps/:id': _.partial(BaseRouter.showViewWithModel, baseRouter, 'l!t!Settings/AddContainerEquipmentRsp', 'models/Settings/ContainerEquipmentRsp', { Equipments: true, }),
			}
        
	        return routes;
	    }
	};

	return factory;
});
