define([
	'base/base-collection',
	'models/InvoiceStornos/InvoiceStornos'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/InvoiceStornos',
		model: Model
	});

	return collection;
});
