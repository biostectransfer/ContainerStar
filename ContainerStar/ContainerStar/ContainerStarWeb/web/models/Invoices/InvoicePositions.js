define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/InvoicePositions',
		fields: {
			id: { type: "number", editable: false }
			,price: { type: "number", 
			                        editable: false, 
			                        validation: { required: true }}
            ,fromDate: { type: "date", 
			                        editable: false, 
				                    validation: { required: false, date: true } }						
			,toDate: { type: "date", 
			                        editable: false, 
				                    validation: { required: false, date: true } }		
            ,description: { type: "string", editable: false }
            ,paymentType: { type: "number", 
			                        editable: false,
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
