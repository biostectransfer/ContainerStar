define([
	'base/base-collection',
	'models/Orders'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/Orders',
		model: Model
	});

	return collection;
});
