define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/InvoicePositions',
		fields: {
			id: { type: "number", editable: false }
			,price: { type: "number", 
			                        editable: Application.canTableItemBeEdit('InvoicePositions', 'price'), 
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
