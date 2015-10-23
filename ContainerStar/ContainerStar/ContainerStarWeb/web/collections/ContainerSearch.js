define([
	'base/base-collection',
	'models/ContainerSearch'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/ContainerSearch',
		model: Model
	});

	return collection;
});
