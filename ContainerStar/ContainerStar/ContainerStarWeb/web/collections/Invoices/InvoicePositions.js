define([
	'base/base-collection',
	'models/Invoices/InvoicePositions'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/InvoicePositions',
		model: Model
	});

	return collection;
});
