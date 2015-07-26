define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: 'api/Invoices',
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
            ,withTaxes: { type: "boolean", 
			                        editable: true, 
				                    validation: { required: false } }			
			,discount: { type: "number", 
			                        editable: true, 
				                    validation: { required: true } }
            ,taxValue: { type: "number", 
			                        editable: false, 
			                        validation: { required: false } }
            ,manualPrice: { type: "number", 
			                        editable: true, 
				                    validation: { required: false } }
            
            ,totalPrice: { type: "number", 
			                        editable: false, 
				                    validation: { required: false } }
            ,payInDays: { type: "number", 
                                    editable: true, 
                                    validation: { required: true } }
            ,payPartOne: { type: "number", 
                                    editable: true, 
                                    validation: { required: false } }
            ,payPartTwo: { type: "number", 
                                    editable: true, 
                                    validation: { required: false } }
            ,payPartTree: { type: "number", 
                                    editable: true, 
                                    validation: { required: false } }
		}
	});
	return model;
});
