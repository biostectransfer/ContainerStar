define(function () {
	'use strict';

	var model = Backbone.Model.extend({
	    urlRoot: Application.apiUrl + '/InvoiceStornos',
		fields: {
		    id: { type: "number", editable: false },
		    invoiceId: { type: "number", editable: false },
			price: { type: "number", editable: true, validation: { required: true }},
			proceedsAccount: { type: "number", editable: true, validation: { required: true } },
		    freeText: { type: "string", editable: true, validation: { required: false } }
		},
	});
	return model;
});
