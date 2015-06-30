define([
	'base/base-collection',
	'models/Invoices/Invoices'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/Invoices',
		model: Model
	});

	return collection;
});
