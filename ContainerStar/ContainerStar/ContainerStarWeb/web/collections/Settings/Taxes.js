define([
	'base/base-collection',
	'models/Settings/Taxes'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/Taxes',
		model: Model
	});

	return collection;
});
