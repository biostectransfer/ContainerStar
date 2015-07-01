define([
	'base/base-collection',
	'models/Invoices/AddInvoicePositions'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/InvoicePositions',
		model: Model
	});

	return collection;
});
