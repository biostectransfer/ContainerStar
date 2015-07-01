define([
	'base/base-collection',
	'models/Invoices/AddInvoice'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/AddInvoice',
		model: Model
	});

	return collection;
});
