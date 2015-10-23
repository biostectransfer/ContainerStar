define([
	'base/base-collection',
	'models/Settings/AdditionalCosts'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/AdditionalCosts',
		model: Model
	});

	return collection;
});
