define([
	'base/base-collection',
	'models/Orders'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/Orders',
		model: Model
	});

	return collection;
});
