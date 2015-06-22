define([
	'base/base-collection',
	'models/Settings/User'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/Users',
		model: Model
	});

	return collection;
});
