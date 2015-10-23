define([
	'base/base-collection',
	'models/Settings/Containers'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/Containers',
		model: Model
	});

	return collection;
});
