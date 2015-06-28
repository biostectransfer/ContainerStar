define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/Positions',
		fields: {
			id: { type: "number", editable: false }
			,orderId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('Positions', 'orderId'), 
				                    validation: { required: true } }		
            ,isSellOrder: { type: "boolean", 
			                        editable: Application.canTableItemBeEdit('Positions', 'isSellOrder'), 
				                    validation: { required: true } }	
			,containerId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('Positions', 'containerId'), 
				                    validation: { required: false } }			
			,additionalCostId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('Positions', 'additionalCostId'), 
				                    validation: { required: false } }			
			,price: { type: "number", 
			                        editable: Application.canTableItemBeEdit('Positions', 'price'), 
				                    validation: { required: true } }				
			,fromDate: { type: "date", 
			                        editable: false, 
				                    validation: { required: false, date: true } }						
			,toDate: { type: "date", 
			                        editable: false, 
				                    validation: { required: false, date: true } }		
            ,description: { type: "string", editable: false }		
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
