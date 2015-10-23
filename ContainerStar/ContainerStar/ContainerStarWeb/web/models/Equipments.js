define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: Application.apiUrl + '/OrderContainerEquipments',
		fields: {
		    id: { type: "number", editable: false }
            ,orderId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('OrderContainerEquipmentRsp', 'containerId'), 
				                    validation: { required: true } }			
			,containerId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('OrderContainerEquipmentRsp', 'containerId'), 
				                    validation: { required: true } }			
			,equipmentId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('OrderContainerEquipmentRsp', 'equipmentId'), 
				                    validation: { required: true } }			
			,amount: { type: "number", 
			                        editable: Application.canTableItemBeEdit('OrderContainerEquipmentRsp', 'amount'), 
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
