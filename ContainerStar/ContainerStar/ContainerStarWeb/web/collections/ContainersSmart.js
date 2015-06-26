define([
	'base/base-collection',
	'models/ContainerSmart'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/ContainerSmart',
		model: Model
	});

	return collection;
});
