define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/Roles',
		fields: {
			id: { type: "number", editable: false }
			,name: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Role', 'name'), 
				                    validation: { required: true, maxLength: 256 } }			
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
