define([
	'base/base-collection',
	'models/Settings/TransportProducts'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/TransportProducts',
		model: Model
	});

	return collection;
});
