define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/Permissions',
		fields: {
			id: { type: "number", editable: false }
			,name: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Permission', 'name'), 
				                    validation: { required: true, maxLength: 256 } }			
			,description: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Permission', 'description'), 
				                    validation: { required: false } }			
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
