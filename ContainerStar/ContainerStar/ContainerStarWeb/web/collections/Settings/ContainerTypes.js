define([
	'base/base-collection',
	'models/Settings/ContainerTypes'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/ContainerTypes',
		model: Model
	});

	return collection;
});
