define([
	'base/base-collection',
	'models/Settings/AdditionalCosts'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/AdditionalCosts',
		model: Model
	});

	return collection;
});
