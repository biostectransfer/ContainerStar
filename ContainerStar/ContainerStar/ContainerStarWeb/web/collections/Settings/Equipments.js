define([
	'base/base-collection',
	'models/Settings/Equipments'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/Equipments',
		model: Model
	});

	return collection;
});
