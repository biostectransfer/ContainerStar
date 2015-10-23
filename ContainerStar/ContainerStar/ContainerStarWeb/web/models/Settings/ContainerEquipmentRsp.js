define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: Application.apiUrl + '/ContainerEquipmentRsps',
		fields: {
			id: { type: "number", editable: false }
			,containerId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('ContainerEquipmentRsp', 'containerId'), 
				                    validation: { required: true } }			
			,equipmentId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('ContainerEquipmentRsp', 'equipmentId'), 
				                    validation: { required: true } }			
			,amount: { type: "number", 
			                        editable: Application.canTableItemBeEdit('ContainerEquipmentRsp', 'amount'), 
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
