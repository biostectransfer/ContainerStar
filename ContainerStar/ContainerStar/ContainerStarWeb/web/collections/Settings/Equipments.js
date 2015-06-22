define([
	'base/base-collection',
	'models/Settings/Equipments'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/Equipments',
		model: Model
	});

	return collection;
});
