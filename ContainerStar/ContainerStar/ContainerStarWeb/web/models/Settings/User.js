define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/Users',
		fields: {
			id: { type: "number", editable: false }
			,roleId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('User', 'roleId'), 
				                    validation: { required: true } }			
			,login: { type: "string", 
			                        editable: Application.canTableItemBeEdit('User', 'login'), 
				                    validation: { required: true, maxLength: 128 } }			
			,name: { type: "string", 
			                        editable: Application.canTableItemBeEdit('User', 'name'), 
				                    validation: { required: true, maxLength: 256 } }			
			,password: { type: "string", 
			                        editable: Application.canTableItemBeEdit('User', 'password'), 
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
