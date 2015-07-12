define([
	'base/base-collection',
	'models/TransportOrders/TransportOrders'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/TransportOrders',
		model: Model
	});

	return collection;
});
