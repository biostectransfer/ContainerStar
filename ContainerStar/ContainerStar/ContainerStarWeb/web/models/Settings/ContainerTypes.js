define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: Application.apiUrl + '/ContainerTypes',
		fields: {
			id: { type: "number", editable: false }
			,name: { type: "string", 
			                        editable: Application.canTableItemBeEdit('ContainerTypes', 'name'), 
				                    validation: { required: true, maxLength: 128 } }			
			,comment: { type: "string", 
			                        editable: Application.canTableItemBeEdit('ContainerTypes', 'comment'), 
				                    validation: { required: false, maxLength: 256 } }			
			,dispositionRelevant: { type: "boolean", 
			                        editable: Application.canTableItemBeEdit('ContainerTypes', 'dispositionRelevant'), 
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
