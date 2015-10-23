define([
	'base/base-collection',
	'models/Settings/Permission'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/Permissions',
		model: Model
	});

	return collection;
});
