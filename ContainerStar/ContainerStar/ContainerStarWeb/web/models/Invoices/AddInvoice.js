define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/AddInvoice',
		fields: {
			id: { type: "number", editable: false }
			,invoiceNumber: { type: "string", 
			                        editable: false, 
				                    validation: { required: false, maxLength: 50 } }			
			,payDate: { type: "date", 
			                        editable: false, 
				                    validation: { required: false, date: true } }	
            ,createDate: { type: "date", 
			                        editable: false, 
				                    validation: { required: true, date: true } }	
            ,customerName: { type: "string", 
			                        editable: false, 
				                    validation: { required: false} }	
            ,communicationPartnerName: { type: "string", 
			                        editable: false, 
				                    validation: { required: false } }	
		}
	});
	return model;
});
