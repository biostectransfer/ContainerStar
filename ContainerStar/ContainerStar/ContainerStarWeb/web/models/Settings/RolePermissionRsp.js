define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/RolePermissionRsps',
		fields: {
			id: { type: "number", editable: false }
			,roleId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('RolePermissionRsp', 'roleId'), 
				                    validation: { required: true } }			
			,permissionId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('RolePermissionRsp', 'permissionId'), 
				                    validation: { required: true } }			
		},
		defaults: function () {
			var dnf = new Date();
			var dnt = new Date(2070,11,31);
			return {
				fromDate: dnf, 
				toDate: dnt
			};
		}
	});
	return model;
});
