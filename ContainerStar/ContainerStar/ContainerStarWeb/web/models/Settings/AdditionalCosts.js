define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/AdditionalCosts',
		fields: {
			id: { type: "number", editable: false }
			,name: { type: "string", 
			                        editable: Application.canTableItemBeEdit('AdditionalCosts', 'name'), 
				                    validation: { required: true, maxLength: 128 } }			
			,description: { type: "string", 
			                        editable: Application.canTableItemBeEdit('AdditionalCosts', 'description'), 
				                    validation: { required: true, maxLength: 128 } }			
			,price: { type: "number", 
			                        editable: Application.canTableItemBeEdit('AdditionalCosts', 'price'), 
				                    validation: { required: true } }			
			,automatic: { type: "boolean", 
			                        editable: Application.canTableItemBeEdit('AdditionalCosts', 'automatic'), 
				                    validation: { required: false } }			
			,includeInFirstBill: { type: "boolean", 
			                        editable: Application.canTableItemBeEdit('AdditionalCosts', 'includeInFirstBill'), 
				                    validation: { required: false } }			
			,proceedsAccount: { type: "number", 
			                        editable: Application.canTableItemBeEdit('AdditionalCosts', 'proceedsAccount'), 
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
