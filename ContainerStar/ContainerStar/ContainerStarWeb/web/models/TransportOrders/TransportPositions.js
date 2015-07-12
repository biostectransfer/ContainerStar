define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/TransportPositions',
		fields: {
			id: { type: "number", editable: false }
			,transportOrderId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('TransportPositions', 'orderId'), 
				                    validation: { required: true } }		
			,transportProductId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('TransportPositions', 'additionalCostId'), 
				                    validation: { required: false } }			
			,price: { type: "number", 
			                        editable: Application.canTableItemBeEdit('TransportPositions', 'price'), 
				                    validation: { required: true } }
            ,amount: { type: "number", 
			                        editable: Application.canTableItemBeEdit('TransportPositions', 'amount'), 
				                    validation: { required: true } }	
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
