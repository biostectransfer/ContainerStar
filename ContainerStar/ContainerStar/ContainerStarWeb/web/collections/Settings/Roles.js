define([
	'base/base-collection',
	'models/Settings/Role'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/Roles',
		model: Model
	});

	return collection;
});
