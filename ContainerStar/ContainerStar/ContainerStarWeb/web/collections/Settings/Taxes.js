define([
	'base/base-collection',
	'models/Settings/Taxes'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/Taxes',
		model: Model
	});

	return collection;
});
