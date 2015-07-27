define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/CommunicationPartners',
		fields: {
			id: { type: "number", editable: false }
			,name: { type: "string", 
			                        editable: Application.canTableItemBeEdit('CommunicationPartners', 'name'), 
				                    validation: { required: true, maxLength: 128 } }			
			,firstName: { type: "string", 
			                        editable: Application.canTableItemBeEdit('CommunicationPartners', 'firstName'), 
				                    validation: { required: false, maxLength: 128 } }			
			,customerId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('CommunicationPartners', 'customerId'), 
				                    validation: { required: true } }			
			,phone: { type: "string", 
			                        editable: Application.canTableItemBeEdit('CommunicationPartners', 'phone'), 
				                    validation: { required: false, maxLength: 20 } }			
			,mobile: { type: "string", 
			                        editable: Application.canTableItemBeEdit('CommunicationPartners', 'mobile'), 
				                    validation: { required: false, maxLength: 20 } }			
			,fax: { type: "string", 
			                        editable: Application.canTableItemBeEdit('CommunicationPartners', 'fax'), 
				                    validation: { required: false, maxLength: 20 } }			
			,email: { type: "string", 
			                        editable: Application.canTableItemBeEdit('CommunicationPartners', 'email'), 
				                    validation: { required: false, maxLength: 50 } }			
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
