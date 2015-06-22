define([
	'base/base-collection',
	'models/Settings/Containers'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/Containers',
		model: Model
	});

	return collection;
});
