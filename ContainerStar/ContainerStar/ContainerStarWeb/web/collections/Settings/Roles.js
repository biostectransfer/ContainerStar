define([
	'base/base-collection',
	'models/Settings/Role'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/Roles',
		model: Model
	});

	return collection;
});
