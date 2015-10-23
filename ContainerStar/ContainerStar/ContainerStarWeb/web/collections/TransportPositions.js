define([
	'base/base-collection',
	'models/TransportOrders/TransportPositions'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/TransportPositions',
		model: Model
	});

	return collection;
});
