define([
	'base/base-collection',
	'models/Invoices/Invoices'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/Invoices',
		model: Model
	});

	return collection;
});
