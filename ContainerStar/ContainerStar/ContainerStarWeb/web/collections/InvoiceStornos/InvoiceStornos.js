define([
	'base/base-collection',
	'models/InvoiceStornos/InvoiceStornos'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/InvoiceStornos',
		model: Model
	});

	return collection;
});
