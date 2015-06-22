define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/Taxes',
		fields: {
			id: { type: "number", editable: false }
			,value: { type: "number", 
			                        editable: Application.canTableItemBeEdit('Taxes', 'value'), 
				                    validation: { required: true } }			
			,fromDate: { type: "date", 
			                        editable: Application.canTableItemBeEdit('Taxes', 'fromDate'), 
				                    validation: { required: true, date: true } }			
			,toDate: { type: "date", 
			                        editable: Application.canTableItemBeEdit('Taxes', 'toDate'), 
				                    validation: { required: true, date: true } }			
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
