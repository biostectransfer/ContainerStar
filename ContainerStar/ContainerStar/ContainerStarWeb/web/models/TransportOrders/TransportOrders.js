define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: Application.apiUrl + '/TransportOrders',
		fields: {
			id: { type: "number", editable: false }
			,customerId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('Orders', 'customerId'), 
				                    validation: { required: true } }			
			,communicationPartnerId: { type: "number", 
			                        editable: Application.canTableItemBeEdit('Orders', 'communicationPartnerId'), 
				                    validation: { required: false } }			
			,deliveryPlace: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Orders', 'deliveryPlace'), 
				                    validation: { required: true, maxLength: 128 } }			
			,street: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Orders', 'street'), 
				                    validation: { required: true, maxLength: 128 } }			
			,zip: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Orders', 'zip'), 
				                    validation: { required: true } }			
			,city: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Orders', 'city'), 
				                    validation: { required: true, maxLength: 128 } }			
			,comment: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Orders', 'comment'), 
				                    validation: { required: false, maxLength: 128 } }			
			,orderDate: { type: "date", 
			                        editable: Application.canTableItemBeEdit('Orders', 'orderDate'), 
				                    validation: { required: false, date: true } }			
			,orderedFrom: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Orders', 'orderedFrom'), 
				                    validation: { required: false, maxLength: 128 } }			
			,orderNumber: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Orders', 'orderNumber'), 
				                    validation: { required: false, maxLength: 50 } }
            ,customerOrderNumber: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Orders', 'customerOrderNumber'), 
				                    validation: { required: false, maxLength: 50 } }			
			,discount: { type: "number", 
			                        editable: Application.canTableItemBeEdit('Orders', 'discount'), 
				                    validation: { required: false } }			
			,billTillDate: { type: "date", 
			                        editable: Application.canTableItemBeEdit('Orders', 'billTillDate'), 
				                    validation: { required: false, date: true } }
			,customerStreet: { type: "string", 
	                                editable: Application.canTableItemBeEdit('Orders', 'customerStreet'), 
	                                validation: { required: true, maxLength: 128 } }			
			,customerZip: { type: "number", 
                                    editable: Application.canTableItemBeEdit('Orders', 'customerZip'), 
                                    validation: { required: true } }			
			,customerCity: { type: "string", 
                                    editable: Application.canTableItemBeEdit('Orders', 'customerCity'), 
                                    validation: { required: true, maxLength: 128 } } 
            ,customerPhone: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Orders', 'customerPhone'), 
				                    validation: { required: false, maxLength: 20 } }				
			,customerFax: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Orders', 'customerFax'), 
				                    validation: { required: false, maxLength: 20 } }			
			,customerEmail: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Orders', 'customerEmail'), 
				                    validation: { required: false, maxLength: 50 } }	
            ,customerNumber: { type: "number", 
			                        editable: Application.canTableItemBeEdit('Orders', 'customerNumber'), 
				                    validation: { required: true } }			
			,newCustomerName: { type: "string", 
			                        editable: Application.canTableItemBeEdit('Orders', 'newCustomerName'), 
				                    validation: { required: true, maxLength: 128 } }
            ,isOffer: { type: "boolean", 
			                        editable: Application.canTableItemBeEdit('Orders', 'isOffer'), 
				                    validation: { required: false } }
            ,createDate: { type: "date", 
			                        editable: false,
			                        validation: { required: false, date: true } }			
            ,status: { type: "number", 
                                    editable: false,
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
