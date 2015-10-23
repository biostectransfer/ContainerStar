define([
	'base/base-collection',
	'models/Positions'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/Positions',
		model: Model
	});

	return collection;
});
