define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/Equipments',
		fields: {
			id: { type: "number", editable: false }
			,description: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Equipments', 'description'), 
				                    validation: { required: true, maxLength: 128 } }			
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
