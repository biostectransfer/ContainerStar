define([
	'base/base-collection',
	'models/Invoices/InvoicePositions'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/InvoicePositions',
		model: Model
	});

	return collection;
});
