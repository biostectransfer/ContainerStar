define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/ContainerTypeEquipmentRsps',
		fields: {
			id: { type: "number", editable: false }
			,containerTypeId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('ContainerTypeEquipmentRsp', 'containerTypeId'), 
				                    validation: { required: true } }			
			,equipmentId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('ContainerTypeEquipmentRsp', 'equipmentId'), 
				                    validation: { required: true } }			
			,amount: { type: "number", 
			                        editable: Application.canTableItemBeEdit('ContainerTypeEquipmentRsp', 'amount'), 
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
