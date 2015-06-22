define([
	'base/base-collection',
	'models/Settings/Customers'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/Customers',
		model: Model
	});

	return collection;
});
