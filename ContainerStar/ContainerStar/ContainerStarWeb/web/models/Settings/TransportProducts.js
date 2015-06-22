define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/TransportProducts',
		fields: {
			id: { type: "number", editable: false }
			,name: { type: "string", 
			                        editable: Application.canTableItemBeEdit('TransportProducts', 'name'), 
				                    validation: { required: true, maxLength: 128 } }			
			,description: { type: "string", 
			                        editable: Application.canTableItemBeEdit('TransportProducts', 'description'), 
				                    validation: { required: true, maxLength: 128 } }			
			,price: { type: "number", 
			                        editable: Application.canTableItemBeEdit('TransportProducts', 'price'), 
				                    validation: { required: true } }			
			,proceedsAccount: { type: "number", 
			                        editable: Application.canTableItemBeEdit('TransportProducts', 'proceedsAccount'), 
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
