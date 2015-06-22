define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/Customers',
		fields: {
			id: { type: "number", editable: false }
			,number: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Customers', 'number'), 
				                    validation: { required: true, maxLength: 20 } }			
			,name: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Customers', 'name'), 
				                    validation: { required: true, maxLength: 128 } }			
			,street: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Customers', 'street'), 
				                    validation: { required: true, maxLength: 128 } }			
			,zip: { type: "number", 
			                        editable: Application.canTableItemBeEdit('Customers', 'zip'), 
				                    validation: { required: true } }			
			,city: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Customers', 'city'), 
				                    validation: { required: true, maxLength: 128 } }			
			,country: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Customers', 'country'), 
				                    validation: { required: false, maxLength: 2 } }			
			,phone: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Customers', 'phone'), 
				                    validation: { required: false, maxLength: 20 } }			
			,mobile: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Customers', 'mobile'), 
				                    validation: { required: false, maxLength: 20 } }			
			,fax: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Customers', 'fax'), 
				                    validation: { required: false, maxLength: 20 } }			
			,email: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Customers', 'email'), 
				                    validation: { required: false, maxLength: 50 } }			
			,comment: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Customers', 'comment'), 
				                    validation: { required: false, maxLength: 128 } }			
			,iban: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Customers', 'iban'), 
				                    validation: { required: false, maxLength: 22 } }			
			,bic: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Customers', 'bic'), 
				                    validation: { required: false, maxLength: 10 } }			
			,withTaxes: { type: "boolean", 
			                        editable: Application.canTableItemBeEdit('Customers', 'withTaxes'), 
				                    validation: { required: false } }			
			,autoDebitEntry: { type: "boolean", 
			                        editable: Application.canTableItemBeEdit('Customers', 'autoDebitEntry'), 
				                    validation: { required: false } }			
			,autoBill: { type: "boolean", 
			                        editable: Application.canTableItemBeEdit('Customers', 'autoBill'), 
				                    validation: { required: false } }			
			,discount: { type: "number", 
			                        editable: Application.canTableItemBeEdit('Customers', 'discount'), 
				                    validation: { required: false } }			
			,ustId: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Customers', 'ustId'), 
				                    validation: { required: false, maxLength: 10 } }			
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
